using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackSpeed;
    public float attackRange;
    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1 / attackSpeed)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        Enemy target = FindTarget();
        if (target != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.Initialize(target);
        }
    }

    Enemy FindTarget()
    {
        // Getting all colliders within the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Enemy closestEnemy = null;
        float closestDistance = attackRange;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
