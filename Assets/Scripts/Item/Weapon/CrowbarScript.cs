using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarScript : LoadWeapon
{
    public ItemObject crowbar;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;

    private float attackCooldown = 0.0f;
    private void Start()
    {
        if (!crowbarExist)
        {
            if (crowbarIsEquipped) { crowbarExist = true; } else { crowbarExist = false; }
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
        Debug.Log("CrowbarAttack");
        animator.SetTrigger("CrowbarAttack");
    }
}
