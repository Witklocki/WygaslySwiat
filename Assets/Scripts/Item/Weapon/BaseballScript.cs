using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballScript : MonoBehaviour
{
    public WeaponObject baseball;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    public FixedJoystick attackJoystick ;

    private float attackCooldown = 0.0f;
    [SerializeField] private bool isAttacking = false;

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

        isAttacking = true;
        animator.SetTrigger("BaseballAttack");

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && isAttacking)
        {
            Debug.Log("BaseballAttack");
            other.gameObject.GetComponent<EnemyAI>().enemy.healthPoint -= baseball.damageBonus;
            Debug.Log(other.gameObject.GetComponent<EnemyAI>().enemy.healthPoint);
        }
    }
}