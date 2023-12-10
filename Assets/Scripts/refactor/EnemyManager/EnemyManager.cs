using System;
using System.Collections.Generic;
using log4net.Util;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // A list or set to keep track of all active enemies
    private HashSet<Enemy> activeEnemies = new HashSet<Enemy>();

    protected override void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerReachedFinalWaypointEvent>(HandleEnemyReachedFinalWaypoint);
        EventBus.Instance.Subscribe<EnemyControllerEnemyDeathEvent>(HandleEnemyDeath);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerReachedFinalWaypointEvent>(HandleEnemyReachedFinalWaypoint);
        EventBus.Instance.Unsubscribe<EnemyControllerEnemyDeathEvent>(HandleEnemyDeath);
    }

    private void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    private void HandleEnemyReachedFinalWaypoint(EnemyControllerReachedFinalWaypointEvent inputEvent)
    {
        // UnregisterEnemy(inputEvent.EnemyController);
    }

    private void HandleEnemyDeath(EnemyControllerEnemyDeathEvent inputEvent)
    {
        // UnregisterEnemy(inputEvent.EnemyController);
    }

    private void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0)
        {
            // No more active enemies; notify EventBus
            EventBus.Instance.Publish(new EnemyManagerAllEnemiesDefeatedEvent());
        }
    }

    // ... Other EnemyManager methods ...
}