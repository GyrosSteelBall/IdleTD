using System;
using System.Collections.Generic;
using log4net.Util;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    // Event to notify when all current enemies have been defeated
    public event Action OnAllEnemiesDefeated;
    // A list or set to keep track of all active enemies
    private HashSet<IEnemy> activeEnemies = new HashSet<IEnemy>();

    protected override void Awake()
    {
        base.Awake();
        WaveManager.Instance.OnSpawnEnemyRequest += HandleSpawnEnemyRequest;
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerEnemyDeathEvent>(UnregisterEnemy);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerEnemyDeathEvent>(UnregisterEnemy);
    }

    private void HandleSpawnEnemyRequest(EnemyData enemyData, Vector3 spawnPoint)
    {
        Debug.Log("HandleSpawnEnemyRequest()");
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
        var newEnemy = newEnemyGameObject.GetComponent<IEnemy>();
        newEnemy?.Initialize(enemyData, spawnPosition);
        activeEnemies.Add(newEnemy);
        // Consider adding a null check or a fallback mechanism
    }

    // Unregister the enemy
    private void UnregisterEnemy(EnemyControllerEnemyDeathEvent inputEvent)
    {
        // enemy.OnDeathEnemy -= () => UnregisterEnemy(enemy);
        // activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0)
        {
            // No more active enemies; notify EventBus
            EventBus.Instance.Publish(new EnemyManagerAllEnemiesDefeatedEvent());
        }
    }

    // ... Other EnemyManager methods ...
}