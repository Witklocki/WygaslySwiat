using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class NPCAIScript : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Rigidbody rb;
    public NavMeshAgent navMeshAgent;
    public PlayerObject playerObject;
    private float radius = 5.0f;

    public float startWaitTime = 2;

    float waitTime;
    public bool playerInRange;
    public bool isPatrol;
    private float colorOpacity = 0.6f;
    private float gloabalDistance = 0;
    Vector3 nextPosition;


    private bool isRight = false;
    private bool isLeft = false;
    private bool isUp = false;
    private bool isDown = false;


    void Start()
    {
        isPatrol = true;
        waitTime = 0;                 //  Set the wait time variable that will change

        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        rb.freezeRotation = true;
        navMeshAgent.updateRotation = false;

        transform.localEulerAngles = new Vector3(45, 0, 0);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 2.0f;
        nextPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(45, 0, 0);
        Patroling();
        navMeshAgent.SetDestination(nextPosition);

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
                Move(2.0f);
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

    void Stop()
    {
        isRight = isLeft = isDown = isUp = false;
        if (animator.gameObject.activeSelf)
        {
            animator.SetBool("isRight", isRight);
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isUp", isUp);
            animator.SetBool("isDown", isDown);
            animator.SetTrigger("isIdle");
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
}
