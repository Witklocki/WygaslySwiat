using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : LoadWeapon
{
    public ItemObject knife;
    public Animator animator;
    public bool canPickup = true;
    public bool isEquipped = false;
    private PlayerObject player;


    private float attackCooldown = 0.0f;
    [SerializeField] private bool isAttacking = false;


    private void Start()
    {
        player = new PlayerObject();

        if (!knifeExist)
        {
            if (knifeIsEquipped) { knifeExist = true; } else { knifeExist = false; }
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
        BoxCollider knifeBox = GetComponent<BoxCollider>();
        knifeBox.enabled = true;
        isAttacking = true;
        animator.SetTrigger("KnifeAttack");
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
