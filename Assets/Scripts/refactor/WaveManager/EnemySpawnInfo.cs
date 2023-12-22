using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Spawn Info", menuName = "WaveManager/Enemy Spawn Info", order = 2)]
public class EnemySpawnInfo : ScriptableObject
{
    public EnemyDataSO enemyData;
    public int count; // Number of enemies of this type to spawn
}