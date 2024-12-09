using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] private float attackRangeX;
    [SerializeField] private float attackRangeY;
    [SerializeField] private float attackCooldown;

    private float cooldownTimer;
    private float attackDamageKnights = 30;
    private float attackDamageAliens = 40;
    private float attackDamageRanges = 50;
    private float attackDamagePaladin = 25;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                cooldownTimer = 0;
            }
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRangeX, attackRangeY), 0, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            switch(enemy.name)
            {
                case "Space":
                    enemy.GetComponent<AlienLife>().TakeDamage(attackDamageAliens);
                    break;
                case "Range":
                    enemy.GetComponent<RangeLife>().TakeDamage(attackDamageRanges);
                    break;
                case "Melee":
                    enemy.GetComponent<KnightLife>().TakeDamage(attackDamageKnights);
                    break;
                case "Paladin":
                    enemy.GetComponent<PaladinLife>().TakeDamage(attackDamagePaladin);
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
