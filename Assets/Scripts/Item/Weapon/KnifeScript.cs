using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public ItemObject knife;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    public FixedJoystick attackJoystick;

    private float attackCooldown = 0.0f;

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
        Debug.Log("KnifeAttack");
        animator.SetTrigger("KnifeAttack");
    }
}
