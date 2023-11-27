using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public EnemyData enemyData;
        public int count; // Number of enemies of this type to spawn
    }

    public List<EnemySpawnInfo> enemySpawns; // List contains info for enemy spawns in this wave
    public float spawnInterval;              // Time between each enemy spawn
    // Add any other properties to define the wave's pattern (e.g., spawn delay, special conditions)
}