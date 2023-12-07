using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// Code responsilbe for operating of enemy component
/// 
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float radius = 15.0f;
    [SerializeField] private bool debug_bool;

    public GameObject dropPrefab;
    private GameObject boomerAttackArea;

    public Animator animator;
    public EnemyObject enemy;
    public Transform player;
    public Rigidbody rb;
    public NavMeshAgent navMeshAgent;
    public PlayerObject playerObject;
    public Sprite attackCircle;

    public float startWaitTime = 2;
    
    public LayerMask playerMask;                    
    public LayerMask obstacleMask;                

    Vector3 nextPosition;

    float waitTime;                               
    public float attackTime;
    public bool playerInRange;                           
    public bool isPatrol;                                
    public bool caughtPlayer;                            
    public bool isEnemyDead;
    public bool attacking;
    private float colorOpacity = 0.6f;
    private float areaAttackVisibilityTime = 1.5f;
    private bool attacked = false;
    private float gloabalDistance = 0;

    /// <summary>
    /// 
    /// </summary>

    private bool isRight = false;
    private bool isLeft = false;
    private bool isUp = false;
    private bool isDown = false;


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

        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().player;
        rb.freezeRotation = true;
        navMeshAgent.updateRotation = false;

        transform.localEulerAngles = new Vector3(45, 0, 0);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = enemy.moveSpeed;
        nextPosition = transform.position;

        animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("Assets/Animations/Enemys/NormalZombiePrefab") as RuntimeAnimatorController;

        animator.SetTrigger("normal");

        boomerAttackArea = new GameObject("BoomerAttackAreaObject", typeof(SpriteRenderer));
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



    /// <summary>
    /// 
    /// 
    /// </summary>
    private void IsPlayerSeen()
    {
        float distance = Vector3.Distance(player.transform.position, transform.transform.position);
        gloabalDistance = distance;
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
        gloabalDistance = distance;
        if (distance < 1.3f || attacking)
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
            }
            else
            {
                Stop();
                if (enemy.type == EnemyType.Boomer)
                {

                }
                attackTime -= Time.deltaTime;
            }
            
        }
        else
        {
            Move(enemy.moveSpeed);
        }
    }

    public void EnemyDead()
    {
        Vector3 dropPosition = transform.position;
        dropPosition.y = -0.1f;
        Instantiate(dropPrefab, dropPosition, Quaternion.identity);
        Destroy(boomerAttackArea);
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
