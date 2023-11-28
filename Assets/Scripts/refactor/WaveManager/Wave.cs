using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "WaveManager/Wave", order = 1)]
public class Wave : ScriptableObject
{
    public List<EnemySpawnInfo> enemySpawns; // List contains info for enemy spawns in this wave
    public float spawnInterval;              // Time between each enemy spawn
    // Add any other properties to define the wave's pattern (e.g., spawn delay, special conditions)
}