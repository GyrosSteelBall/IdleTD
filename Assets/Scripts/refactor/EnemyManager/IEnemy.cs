using System;
using UnityEngine;

public interface IEnemy
{
    void Initialize(EnemyData enemyData, Vector3 spawnPosition);
    public event Action OnDeathEnemy;
    // Add other methods that every enemy should implement
}
