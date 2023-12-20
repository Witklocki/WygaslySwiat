using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseballScript : LoadWeapon
{
    public WeaponObject baseball;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    private PlayerObject player;


    private float attackCooldown = 0.0f;
    [SerializeField] private bool isAttacking = false;


    private void Start()
    {
        player = new PlayerObject();
        if (!baseballExist)
        {
            if (baseballIsEquipped) { baseballExist = true; }else { baseballExist = false; }
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if (isEquipped)
        {

            if (attackJoystick.Vertical != 0 || attackJoystick.Horizontal != 0)
            { 
                // Check if the attack cooldown has passed
                if (attackCooldown <= 0f)
                {
                    PlayerAttack();

                    attackCooldown = 0.5f;
                }
            }
            else
            {
                isAttacking = false;
            }

            // Reduce the attack cooldown timer
            if (attackCooldown > 0f)
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }

    void PlayerAttack()
    {
        animator = GetComponentInParent<Animator>();
        BoxCollider baseballBox = GetComponent<BoxCollider>();
        baseballBox.enabled = true;
        isAttacking = true;
        animator.SetTrigger("BaseballAttack");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && isAttacking)
        {
            other.gameObject.GetComponent<EnemyAI>().enemy.healthPoint -= player.attack;
            isAttacking = false;

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy" && isAttacking)
        {
            other.gameObject.GetComponent<EnemyAI>().enemy.healthPoint -= player.attack;
            isAttacking = false;

        }
    }
}
