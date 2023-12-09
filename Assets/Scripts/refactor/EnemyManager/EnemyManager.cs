using System;
using System.Collections.Generic;
using log4net.Util;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // Event to notify when all current enemies have been defeated
    public event Action OnAllEnemiesDefeated;
    // A list or set to keep track of all active enemies
    private HashSet<IEnemyController> activeEnemies = new HashSet<IEnemyController>();
    [SerializeField] private PathManager pathManager;

    protected override void Awake()
    {
        base.Awake();
        WaveManager.Instance.OnSpawnEnemyRequest += HandleSpawnEnemyRequest;
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

    private void HandleSpawnEnemyRequest(EnemyData enemyData, Vector3 spawnPoint)
    {
        SpawnEnemy(enemyData, spawnPoint);
    }

    public void SpawnEnemy(EnemyData enemyData, Vector3 spawnPosition)
    {
        var enemyPrefab = enemyData.EnemyPrefab;
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is missing for the given EnemyData object.");
            return;
        }

        var newEnemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        var newEnemy = newEnemyGameObject.GetComponent<EnemyController>();
        if (newEnemy != null)
        {
            var pathIndex = 0; //Maybe update this later to be configurable
            newEnemy.Initialize(enemyData);
            AssignPathToEnemy(newEnemy, pathIndex);
            activeEnemies.Add(newEnemy);
        }
        else
        {
            Debug.LogError("EnemyController component not found on the spawned enemy prefab.");
        }
        // Consider adding a null check or a fallback mechanism
    }

    private void AssignPathToEnemy(EnemyController enemy, int pathIndex)
    {
        var path = pathManager.GetPath(pathIndex);
        enemy.SetPath(path);
    }

    private void HandleEnemyReachedFinalWaypoint(EnemyControllerReachedFinalWaypointEvent inputEvent)
    {
        UnregisterEnemy(inputEvent.EnemyController);
    }

    private void HandleEnemyDeath(EnemyControllerEnemyDeathEvent inputEvent)
    {
        UnregisterEnemy(inputEvent.EnemyController);
    }

    private void UnregisterEnemy(EnemyController enemy)
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