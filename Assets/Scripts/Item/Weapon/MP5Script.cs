using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5Script : LoadWeapon
{
    public ItemObject mp5;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    public Sprite oldSprite;
    public Sprite newSprite;
    private float attackCooldown = 0.0f;
    //Shooting
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    private void Start()
    {
        if (!MP5Exist)
        {
            if (MP5IsEquipped) { MP5Exist = true; } else { MP5Exist = false; }
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

                    attackCooldown = 0.3f;
                }
            }
            if (attackCooldown <= 0.2f)
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
        animator = GetComponentInParent<Animator>();
        Debug.Log("Mp5Attack");
        animator.SetTrigger("MP5Attack");
        SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (childSpriteRenderer != null)
        {
            // Change the sprite of the child SpriteRenderer
            childSpriteRenderer.sprite = newSprite;
        }
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
