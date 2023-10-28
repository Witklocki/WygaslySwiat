using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public FixedJoystick moveJoystick;
    public Rigidbody rb;
    public Map map;
    public PlayerObject player;

    public float groundDrag;
    public GameObject equippedWeaponSlot;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void Update()
    {
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
        movePlayer();

    }

    private void movePlayer()
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

        rb.MovePosition((Vector3)transform.position + (direction * player.moveSpeed * Time.deltaTime));


        if (direction != Vector3.zero)
        {
            // Calculate the rotation based on the movement direction

            Quaternion targetRotation = Quaternion.Euler(45, 0, Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);

            if (equippedWeaponSlot != null)
            {

                Vector3 playerPosition = transform.position;
                Vector3 localWeaponPosition = new Vector3(circleRadius, weaponHeight, -weaponDepth);

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
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 

        if(flatVelocity.magnitude > player.moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * player.moveSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y, limitedVel.z);
        }
    }
}
