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
    private SpriteRenderer boomerAttackArea;

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
    public bool attacking;
    private float colorOpacity = 0.6f;
    private float areaAttackVisibilityTime = 1.5f;
    private bool attacked = false;


    // Start is called before the first frame update
    void Start()
    {

        isPatrol = true;
        caughtPlayer = false;
        playerInRange = false;
        attacking = false;
        waitTime = 0;                 //  Set the wait time variable that will change
        attackTime = startWaitTime;

        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        playerObject = AssetDatabase.LoadAssetAtPath<PlayerObject>("Assets/Scripts/Player/PlayerData.asset");
        rb.freezeRotation = true;

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = enemy.moveSpeed;
        nextPosition = transform.position;

        GameObject boomerAttackAreaObject = new GameObject("BoomerAttackAreaObject", typeof(SpriteRenderer));
        boomerAttackArea = boomerAttackAreaObject.GetComponent<SpriteRenderer>();
        boomerAttackArea.sprite = attackCircle;
        boomerAttackArea.color = new Color(1, 0, 0, 0.0f);
        boomerAttackArea.transform.position = transform.position;
        boomerAttackArea.transform.Rotate(90.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
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
        if(boomerAttackArea != null)
        {
            areaAttackVisibilityTime -= Time.deltaTime;
            if (areaAttackVisibilityTime <= 0)
            {
                Destroy(boomerAttackArea);
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (debug_bool)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nextPosition);
        }
        if (attacking)
        {
            Gizmos.DrawSphere(transform.position, 3.0f);
        }
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
        float distance = Vector3.Distance(player.transform.position, transform.transform.position);
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
                    boomerAttackArea.color = new Color(1, 0, 0, 0.6f);
                    boomerAttackArea.transform.localScale = new Vector3(3, 3);
                    boomerAttackArea.transform.position = transform.position;
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
}
