using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballScript : MonoBehaviour
{
    public ItemObject baseball;
    public Animator animator;
    public bool canPickup = true;
    public FixedJoystick attackJoystick ;

    private float attackCooldown = 0.0f;

    private void Update()
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

        // Reduce the attack cooldown timer
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void PlayerAttack()
    {
        Debug.Log("PlayerAttacked");
        animator.SetTrigger("Attack");
    }
}
