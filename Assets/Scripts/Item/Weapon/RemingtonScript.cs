using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemingtonScript : MonoBehaviour
{
    public ItemObject remington;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    public FixedJoystick attackJoystick;
    public Sprite oldSprite;
    public Sprite newSprite;
    private float attackCooldown = 0.0f;
    //Shooting
    public Transform shootingPoint;
    public GameObject bulletPrefab;
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
            if (attackCooldown <= 0.3f)
            {
                SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
                childSpriteRenderer.sprite = oldSprite;
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
        Debug.Log("RemingtonAttack");
        animator.SetTrigger("RemingtonAttack");
        SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (childSpriteRenderer != null)
        {
            // Change the sprite of the child SpriteRenderer
            childSpriteRenderer.sprite = newSprite;
        }
        for (int i = 0; i < 5; i++)
        {
            float randomAngle = Random.Range(-22.5f, 22.5f); // Random angle within a 45-degree cone
            Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, 0f, randomAngle);
            Instantiate(bulletPrefab, shootingPoint.position, finalRotation);
        }
    }
}
