using UnityEngine;

public class WaveManagerSpawnEnemyRequestEvent
{
    public EnemyData EnemyData { get; private set; }
    public Vector3 SpawnPoint { get; private set; }

    public WaveManagerSpawnEnemyRequestEvent(EnemyData enemyData, Vector3 spawnPoint)
    {
        EnemyData = enemyData;
        SpawnPoint = spawnPoint;
    }
}