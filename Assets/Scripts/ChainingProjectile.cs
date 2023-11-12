using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainingProjectile : Projectile
{
    public int maxChains;
    private int currentChains;
    public float maxChainDistance = 10f; // Maximum distance to find the next target
    private List<Enemy> hitEnemies = new List<Enemy>(); // List to keep track of hit enemies

    public override void HitTarget()
    {
        if (currentChains < maxChains)
        {
            target.TakeDamage(damage);
            hitEnemies.Add(target); // Add the target to the list of hit enemies
            currentChains++;

            // Logic to find and set the next target goes here
            FindAndSetNextTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FindAndSetNextTarget()
    {
        Enemy[] enemies = GetAllEnemies();
        Enemy closestEnemy = null;
        float closestDistance = maxChainDistance;

        foreach (Enemy enemy in enemies)
        {
            if (!hitEnemies.Contains(enemy) && !enemy.IsDead) // Ensure the enemy hasn’t already been hit
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance && enemy != target)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
        }
        else
        {
            Destroy(gameObject); // Destroy the projectile if no suitable target is found
        }
    }

    // Get all game objects with the Enemy script attached
    private Enemy[] GetAllEnemies()
    {
        return GameObject.FindObjectsOfType<Enemy>();
    }
}