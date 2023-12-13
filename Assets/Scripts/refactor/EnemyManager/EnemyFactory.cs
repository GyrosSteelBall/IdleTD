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

        GameObject newEnemyGameObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController newEnemyController = newEnemyGameObject.GetComponent<EnemyController>();
        EnemyAnimator newEnemyAnimator = newEnemyGameObject.GetComponent<EnemyAnimator>();


        if (newEnemyController == null)
        {
            newEnemyController = newEnemyGameObject.AddComponent<EnemyController>();
        }

        if (newEnemyAnimator == null)
        {
            newEnemyAnimator = newEnemyGameObject.AddComponent<EnemyAnimator>();
            newEnemyAnimator.SetController(newEnemyController);
        }

        Enemy createdEnemy = new Enemy(newEnemyController, newEnemyAnimator);
        createdEnemy.Controller.ParentEnemy = createdEnemy;

        // hard-coded path index for now
        AssignPathToEnemy(createdEnemy, 0);

        return createdEnemy;
    }

    private void AssignPathToEnemy(Enemy enemy, int pathIndex)
    {
        var path = PathManager.Instance.GetPath(pathIndex);
        enemy.Controller.SetPath(path);
    }
}