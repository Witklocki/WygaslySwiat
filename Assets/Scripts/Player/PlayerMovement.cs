using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public FixedJoystick moveJoystick;
    //public FixedJoystick attackJoystick;
    public Rigidbody rb;
    public Map map;
    public PlayerObject player;
    public float groundDrag;
    public GameObject equippedWeaponSlot;
    public HealthBar healthBar;
    public DropObjectController dropObject;

    // Ensure only one player is created

    private static bool playerCreated = false;
    private bool isRight;
    private bool isLeft;
    private bool isUp;
    private bool isDown;
    public Vector3 direction;

    public float circleRadius = 0.5f;
    public float weaponHeight = 0.5f; // Set the desired Y position
    public float weaponDepth = 0.8f;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [SerializeField] DB dataBase;


    private void Awake()
    {
        player.readJson();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dropObject = new DropObjectController(0, 0, 0, 0);

        player.readJson();

        if (!playerCreated)
        {
            if (map.IsUnityNull())
            {
                map = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<Terrain>().GetComponent<Map>();
            }

            rb = GetComponent<Rigidbody>();
            player.healthPoint = player.maxHealth;
            playerCreated = true;
        }
        else
        {
            // If playerCreated is true, destroy the duplicate player
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Suburbs") 
        {
            StartCoroutine(SetPlayerPosition());
            FindAndAssignJoystick<FixedJoystick>(ref moveJoystick, "MoveJoystick");
            FindAndAssignMap<Map>("Terrain");
            dataBase.npcSavingInProgres = false;
        }
        if (scene.name == "SavePlace")
        {
            StartCoroutine(SetPlayerPosition());
            FindAndAssignJoystick<FixedJoystick>(ref moveJoystick, "MoveJoystick");
            FindAndAssignMap<Map>("Terrain");
            if (dataBase.npcSavingInProgres)
            {
                dataBase.NPCList.npcIsSaved();
            }
        }
    }
    private void FindAndAssignJoystick<T>(ref T joystick, string joystickName) where T : FixedJoystick
    {
        // Assuming the Canvas is a top-level object in the scene
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            // Find the FixedJoystick within the Canvas
            FixedJoystick[] joysticks = canvas.GetComponentsInChildren<FixedJoystick>();

            // Assuming you have only one FixedJoystick, you might need to adjust this logic if there are multiple
            if (joysticks.Length > 0)
            {
                moveJoystick = joysticks[0];
            }
            else
            {
                Debug.LogWarning("FixedJoystick not found in the Canvas.");
            }
            // Find the AttackJoystick within the Canvas
            FixedJoystick[] attackJoysticks = canvas.GetComponentsInChildren<FixedJoystick>();
            
            

            // Assuming you have only one AttackJoystick, you might need to adjust this logic if there are multiple
            if (attackJoysticks.Length > 0)
            {
                //attackJoystick = attackJoysticks[0];
            }
            else
            {
                Debug.LogWarning("AttackJoystick not found in the Canvas.");
            }

            HealthBar tmpHealthBars = canvas.GetComponentInChildren<HealthBar>();

            if (tmpHealthBars != null)
            {
                healthBar = tmpHealthBars;
                SetHealth();
            }
            else
            {
                Debug.LogWarning("Health bar not found in the Canvas.");
            }

        }
        else
        {
            Debug.LogWarning("Canvas not found in the scene.");
        }
    }
    private void FindAndAssignMap<T>(string mapName) where T : Map
    {
        Map[] maps = FindObjectsOfType<T>();

        if (maps.Length > 0)
        {
            map = maps[0];
        }
        else
        {
            Debug.LogWarning($"{mapName} not found in the scene.");
        }
    }

    private IEnumerator SetPlayerPosition()
    {
        // Wait for one frame to ensure all objects are properly initialized
        yield return null;

        transform.position = SpawnManager.Instance.customSpawnPosition;
    }
    private void Update()
    {
        healthBar.SetHealth(player.healthPoint);

        if (player.healthPoint <= 0)
        {
            PlayerIsDead();
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        PlayerSpeedControll();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = moveJoystick.Horizontal;
        float vertical = moveJoystick.Vertical;
        isRight = (horizontal > 0) && (Mathf.Abs(horizontal) > Mathf.Abs(vertical));
        isLeft = (horizontal < 0) && (Mathf.Abs(horizontal) > Mathf.Abs(vertical));
        isUp = (vertical > 0) && (Mathf.Abs(vertical) > Mathf.Abs(horizontal));
        isDown = (vertical < 0) && (Mathf.Abs(vertical) > Mathf.Abs(horizontal));
        animator.SetBool("isRight", isRight);
        animator.SetBool("isLeft", isLeft);
        animator.SetBool("isUp", isUp);
        animator.SetBool("isDown", isDown);

        animator.SetFloat("SpeedH", Mathf.Abs(horizontal));
        animator.SetFloat("Speed", Mathf.Abs(vertical));

         direction = new Vector3(horizontal, 0, vertical).normalized;

        rb.MovePosition((Vector3)transform.position + (player.moveSpeed * Time.deltaTime * direction));


        if (direction != Vector3.zero)
        {
            // Calculate the rotation based on the movement direction

            Quaternion targetRotation = Quaternion.Euler(45, 0, Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);

            if (equippedWeaponSlot != null)
            {

                Vector3 playerPosition = transform.position;
                Vector3 localWeaponPosition = new(circleRadius, weaponHeight, -weaponDepth);

                // Rotate the local position based on the player's rotation
                localWeaponPosition = Quaternion.Euler(0, 0, 0) * localWeaponPosition;

                // Transform local position to world space
                Vector3 weaponPosition = playerPosition + targetRotation * localWeaponPosition;

                equippedWeaponSlot.transform.position = weaponPosition;
                equippedWeaponSlot.transform.rotation = targetRotation; 
            }
        }


        if (rb.position.x > map.map.rangX)
        {
            rb.MovePosition(new Vector3(map.map.rangX, rb.position.y, rb.position.z));
            print("limit");
            /*rb.position.x = map.map.rangX;*/
        }
        else if (rb.position.x < -map.map.rangX)
        {
            rb.MovePosition(new Vector3(-map.map.rangX, rb.position.y, rb.position.z));
            print("limit");
        }
        if (rb.position.z > map.map.rangY)
        {
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y, map.map.rangY));
            print("limit");
            /*rb.position.x = map.map.rangX;*/
        }
        else if (rb.position.z < -map.map.rangY)
        {
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y,-map.map.rangY));
            print("limit");
        }
        /*transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);*/
    }

    void PlayerSpeedControll()
    {
        Vector3 flatVelocity = new(rb.velocity.x, 0f, rb.velocity.z); 

        if(flatVelocity.magnitude > player.moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * player.moveSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y, limitedVel.z);
        }
    }

    void PlayerIsDead()
    {
        SceneManager.LoadScene("SavePlace",LoadSceneMode.Single);
        player.healthPoint = player.maxHealth;
        SetHealth();
        Instantiate(gameObject);
    }

    void SetHealth()
    {
        if (healthBar != null )
        {
            healthBar.SetMaxHealth(player.maxHealth);
        }
    }

}
