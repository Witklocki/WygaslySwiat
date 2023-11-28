using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewolwerScript : LoadWeapon
{
    public ItemObject rewolwer;
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
        if (!rewolwerExist)
        {
            if (rewolwerIsEquipped) { rewolwerExist = true; } else { rewolwerExist = false; }
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
        Debug.Log("RewolwerAttack");
        animator.SetTrigger("RewolwerAttack");
        SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (childSpriteRenderer != null)
        {
            // Change the sprite of the child SpriteRenderer
            childSpriteRenderer.sprite = newSprite;
        }
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
