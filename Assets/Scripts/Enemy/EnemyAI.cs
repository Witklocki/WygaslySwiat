using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float radius = 15.0f;
    [SerializeField] private bool debug_bool;

    public Animator animator;
    public EnemyObject enemy;
    public Transform player;
    public Rigidbody rb;
    public NavMeshAgent navMeshAgent;
    public PlayerObject playerObject;
    public Sprite attackCircle;

    public float startWaitTime = 2;
    
    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask obstacleMask;                  //  To detect the obstacules with the raycast

    Vector3 nextPosition;

    float waitTime;                               //  Variable of the wait time that makes the delay
    public float attackTime;
    public bool playerInRange;                           //  If the player is in range of vision, state of chasing
    public bool isPatrol;                                //  If the enemy is patrol, state of patroling
    public bool caughtPlayer;                            //  if the enemy has caught the player
    public bool isEnemyDead;
    public bool attacking;
    private float colorOpacity = 0.6f;
    private float areaAttackVisibilityTime = 1.5f;
    private bool attacked = false;

    [SerializeField] private bool isRight = false;
    [SerializeField] private bool isLeft = false;
    [SerializeField] private bool isUp = false;
    [SerializeField] private bool isDown = false;


    // Start is called before the first frame update
    void Start()
    {

        isPatrol = true;
        isEnemyDead = caughtPlayer = false;
        playerInRange = false;
        attacking = false;
        waitTime = 0;                 //  Set the wait time variable that will change
        attackTime = startWaitTime;

        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        playerObject = AssetDatabase.LoadAssetAtPath<PlayerObject>("Assets/Scripts/Player/PlayerData.asset");
        rb.freezeRotation = true;
        navMeshAgent.updateRotation = false;

        transform.localEulerAngles = new Vector3(45, 0, 0);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = enemy.moveSpeed;
        nextPosition = transform.position;

    /*    animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("Assets/Animations/Enemys/NormalZombiePrefab") as RuntimeAnimatorController;
*/
        animator.SetTrigger("normal");

        GameObject boomerAttackAreaObject = new GameObject("BoomerAttackAreaObject", typeof(SpriteRenderer));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(45, 0, 0);

        if (isEnemyDead)
        {
            EnemyDead();
        }

        IsPlayerSeen();
        if (!isPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
        navMeshAgent.SetDestination(nextPosition);

    }

    private void OnDrawGizmos()
    {
        if (debug_bool)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nextPosition);
        }
       /* if (attacking)
        {
            Gizmos.DrawSphere(transform.position, 3.0f);
        }*/
    }



    private void IsPlayerSeen()
    {
        float distance = Vector3.Distance(player.transform.position, transform.transform.position);
        if(distance <= enemy.viewRadius)
        {
            playerInRange = true;
            isPatrol = false;
        }
        else
        {
            playerInRange = false;
            isPatrol = true;
        }
    }

    void Stop()
    {
        isRight = isLeft = isDown = isUp = false; 
        if (animator.gameObject.activeSelf)
        {
            animator.SetBool("isRight", isRight);
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isUp", isUp);
            animator.SetBool("isDown", isDown);
            animator.SetTrigger("Idle");
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
    

        navMeshAgent.speed = speed;
    }

    void Patroling()
    {
        checkingDirection();
        if (Vector3.Distance(nextPosition, transform.position) <= 1.5f && !playerInRange)
        {
            if (waitTime <= 0)
            {
                Move(enemy.moveSpeed);
                waitTime = startWaitTime;
                nextPosition = RandomPointGenerator.RandomPointGenerate(transform.position, radius);

            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    void Chasing()
    {
        nextPosition = player.position;
        checkingDirection();
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 0.9f || attacking)
        {
            caughtPlayer = true;
            attacking = true;
            Attack();
        }
        else
        {
            Move(enemy.runSpeed);
            attackTime = startWaitTime;
            caughtPlayer = false;
        }
    }

    void Attack()
    {
        if (caughtPlayer)
        {
            if (attackTime <=0)
            {
                Move(enemy.moveSpeed);
                playerObject.healthPoint -= enemy.attack * enemy.attackMultiplayer;
                attackTime = startWaitTime;
                caughtPlayer = false;
                attacking = false;
                if (this.enemy.type == EnemyType.Boomer)
                {
                    attacked = true;

                }
                Debug.Log(playerObject.healthPoint);
            }
            else
            {
                Stop();
                if (enemy.type == EnemyType.Boomer)
                {

                }
                attackTime -= Time.deltaTime;
                Debug.Log("Attack");
            }
            
        }
        else
        {
            Move(enemy.moveSpeed);
        }
    }

    public void EnemyDead()
    {
        Destroy(gameObject);
    }

    void IsEnemyDead()
    {
        if (enemy.healthPoint <= 0)
        {
            playerInRange = false;
            attacking = false;
            caughtPlayer = false;
            isPatrol = false;   
            isEnemyDead = true;
        }
    }

    void checkingDirection()
    {
        Vector3 direction = (nextPosition - transform.position).normalized;
        Debug.Log(direction);
        isRight = direction.x > 0 && Math.Abs(direction.x) > Math.Abs(direction.z);
        isLeft = direction.x < 0 && Math.Abs(direction.x) > Math.Abs(direction.z);
        isUp = direction.z > 0 && Math.Abs(direction.z) > Math.Abs(direction.x);
        isDown = direction.z < 0 && Math.Abs(direction.z) > Math.Abs(direction.x);

        if (animator.gameObject.activeSelf)
        {
            animator.SetBool("isRight", isRight);
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isUp", isUp);
            animator.SetBool("isDown", isDown);
        }
    }
}
