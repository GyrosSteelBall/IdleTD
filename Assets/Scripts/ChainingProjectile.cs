using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChainingProjectile : Projectile
{
    [SerializeField] private int maxChains;
    [SerializeField] private float maxChainDistance = 10f;
    private List<Enemy> hitEnemies = new List<Enemy>();
    private Enemy[] allEnemies;
    private int currentChains;

    protected override void Start()
    {
        base.Start();
        // Cache all enemies when the projectile is created
        allEnemies = FindAllEnemies();
    }

    protected override void HitTarget()
    {
        target.TakeDamage(damage);
        hitEnemies.Add(target);
        currentChains++;

        if (currentChains >= maxChains)
        {
            DestroyProjectile();
            return;
        }

        var nextTarget = FindNextTarget();
        if (nextTarget != null)
        {
            target = nextTarget;
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
        // If you have a delegate or Unity event set up to respond to destruction, invoke it here.
    }

    private Enemy FindNextTarget()
    {
        return allEnemies
            .Where(e => !hitEnemies.Contains(e) && !e.IsDead && e != target)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
    }

    private static Enemy[] FindAllEnemies()
    {
        return GameObject.FindObjectsOfType<Enemy>();
    }
}