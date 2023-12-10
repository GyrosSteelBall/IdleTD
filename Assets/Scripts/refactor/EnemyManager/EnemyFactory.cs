using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{
    void OnEnable()
    {
        EventBus.Instance.Subscribe<WaveManagerSpawnEnemyRequestEvent>(HandleSpawnEnemyRequest);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<WaveManagerSpawnEnemyRequestEvent>(HandleSpawnEnemyRequest);
    }

    private void HandleSpawnEnemyRequest(WaveManagerSpawnEnemyRequestEvent inputEvent)
    {
        Enemy newEnemy = CreateEnemy(inputEvent.EnemyData, inputEvent.SpawnPoint);
        if (newEnemy != null)
        {
            EventBus.Instance.Publish(new EnemyFactoryEnemySpawnedEvent(newEnemy));
        }
    }

    public Enemy CreateEnemy(EnemyData enemyData, Vector3 spawnPosition)
    {
        var enemyPrefab = enemyData.EnemyPrefab;
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is missing for the given EnemyData object.");
            return null;
        }

        var newEnemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        var newEnemyController = newEnemyGameObject.GetComponent<EnemyController>();

        if (newEnemyController == null)
        {
            newEnemyController = newEnemyGameObject.AddComponent<EnemyController>();
        }

        // hard-coded path index for now
        AssignPathToEnemy(newEnemyController, 0);

        return new Enemy(newEnemyController);
    }

    private void AssignPathToEnemy(EnemyController enemyController, int pathIndex)
    {
        var path = PathManager.Instance.GetPath(pathIndex);
        enemyController.SetPath(path);
    }
}