using System;
using System.Collections.Generic;
using log4net.Util;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // Event to notify when all current enemies have been defeated
    public event Action OnAllEnemiesDefeated;
    // A list or set to keep track of all active enemies
    private HashSet<EnemyController> activeEnemies = new HashSet<EnemyController>();

    protected override void Awake()
    {
        base.Awake();
        WaveManager.Instance.OnSpawnEnemyRequest += HandleSpawnEnemyRequest;
    }

    private void HandleSpawnEnemyRequest(EnemyData enemyData, Vector3 spawnPoint)
    {
        Debug.Log("HandleSpawnEnemyRequest()");
        SpawnEnemy(enemyData, spawnPoint);
    }

    // Public method to spawn an enemy
    public void SpawnEnemy(EnemyData enemyData, Vector3 spawnPosition)
    {
        var enemyPrefab = enemyData.EnemyPrefab;
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is missing for the given EnemyData object.");
            return;
        }

        var newEnemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        // var newEnemyController = newEnemyGameObject.GetComponent<EnemyController>();
        // Initialize the enemy with any additional parameters if necessary
        // Register this enemy
        // activeEnemies.Add(newEnemyController);
        // newEnemyController.OnDeath += () => UnregisterEnemy(newEnemyController);
    }

    // Unregister the enemy
    private void UnregisterEnemy(EnemyController enemy)
    {
        enemy.OnDeath -= () => UnregisterEnemy(enemy);
        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0)
        {
            // No more active enemies; notify GameManager or WaveManager
            OnAllEnemiesDefeated?.Invoke();
        }
    }

    // ... Other EnemyManager methods ...
}