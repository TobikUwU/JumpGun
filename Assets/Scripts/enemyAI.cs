using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyAI : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;

    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;

    private Health playerHealth;

    private Animator anim;
    

    private float attackTimer = Mathf.Infinity;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(attackTimer >= attackCooldown){

                attackTimer = 0;
                anim.SetTrigger("attack");
                //attack
            }
        }
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x, boxCollider.bounds.size, 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x, boxCollider.bounds.size);
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            Console.WriteLine("hit");
            playerHealth.TakeDamage(attackDamage);

        }

    }
}
