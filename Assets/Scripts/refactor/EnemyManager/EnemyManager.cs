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
        //listen for enemy factory spawning
        EventBus.Instance.Subscribe<EnemyFactoryEnemySpawnedEvent>(HandleEnemySpawned);
        EventBus.Instance.Subscribe<EnemyControllerReachedFinalWaypointEvent>(HandleEnemyReachedFinalWaypoint);
        EventBus.Instance.Subscribe<EnemyControllerEnemyDeathEvent>(HandleEnemyDeath);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyFactoryEnemySpawnedEvent>(HandleEnemySpawned);
        EventBus.Instance.Unsubscribe<EnemyControllerReachedFinalWaypointEvent>(HandleEnemyReachedFinalWaypoint);
        EventBus.Instance.Unsubscribe<EnemyControllerEnemyDeathEvent>(HandleEnemyDeath);
    }

    private void HandleEnemySpawned(EnemyFactoryEnemySpawnedEvent inputEvent)
    {
        RegisterEnemy(inputEvent.Enemy);
    }

    private void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    private void HandleEnemyReachedFinalWaypoint(EnemyControllerReachedFinalWaypointEvent inputEvent)
    {
        UnregisterEnemy(inputEvent.EnemyController.ParentEnemy);
    }

    private void HandleEnemyDeath(EnemyControllerEnemyDeathEvent inputEvent)
    {
        UnregisterEnemy(inputEvent.EnemyController.ParentEnemy);
    }

    private void UnregisterEnemy(Enemy enemy)
    {
        Debug.Log("Unregistering enemy");
        activeEnemies.Remove(enemy);
        Debug.Log("Active enemies: " + activeEnemies.Count);

        if (activeEnemies.Count == 0)
        {
            // No more active enemies; notify EventBus
            EventBus.Instance.Publish(new EnemyManagerAllEnemiesDefeatedEvent());
        }
    }

    // ... Other EnemyManager methods ...
}