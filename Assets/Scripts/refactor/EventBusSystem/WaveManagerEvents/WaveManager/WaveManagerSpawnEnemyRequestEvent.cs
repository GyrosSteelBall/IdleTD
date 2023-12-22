using UnityEngine;

public class WaveManagerSpawnEnemyRequestEvent
{
    public EnemyDataSO EnemyData { get; private set; }
    public Vector3 SpawnPoint { get; private set; }

    public WaveManagerSpawnEnemyRequestEvent(EnemyDataSO enemyData, Vector3 spawnPoint)
    {
        EnemyData = enemyData;
        SpawnPoint = spawnPoint;
    }
}