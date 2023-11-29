using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController
{
    private EnemyData enemyData;
    public event Action OnDeath;
    public event Action OnDeathEnemyController;

    public void Initialize(EnemyData data)
    {
        enemyData = data;
        // Initialize logic-specific properties based on EnemyData
    }

    void Update()
    {
        // Handle behavior such as moving along a path and attacking targets
    }

    public void TakeDamage(float damage)
    {
        // Reduce health and possibly trigger death
    }

    public void Die()
    {
        OnDeathEnemyController?.Invoke();
    }

    public void Move(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    // Other methods like moving, attacking, etc.
}