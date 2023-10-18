using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public List<Unit> Units { get; private set; } = new List<Unit>();
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    void Update()
    {
        HandleCombat();
    }

    private void HandleCombat()
    {
        foreach (var unit in Units)
        {
            Enemy target = FindClosestEnemyWithinRange(unit);
            if (target != null)
            {
                unit.Attack(target);
            }
        }
    }

    private Enemy FindClosestEnemyWithinRange(Unit unit)
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;  // Start with the maximum possible distance

        foreach (var enemy in Enemies)
        {
            float distance = Vector3.Distance(unit.Position, enemy.Position);
            if (distance <= unit.AttackRange && distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }


    public void RegisterUnit(Unit unit)
    {
        Units.Add(unit);
        unit.OnDeath += HandleUnitDeath;
    }

    public void DeregisterUnit(Unit unit)
    {
        Units.Remove(unit);
        unit.OnDeath -= HandleUnitDeath;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.OnDeath += HandleEnemyDeath;
    }

    public void DeregisterEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.OnDeath -= HandleEnemyDeath;
    }

    private void HandleUnitDeath(Unit unit)
    {
        DeregisterUnit(unit);
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        DeregisterEnemy(enemy);
    }
}
