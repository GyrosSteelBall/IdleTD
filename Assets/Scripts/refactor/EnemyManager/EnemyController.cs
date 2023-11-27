using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyData enemyData;
    public event Action OnDeath;

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

    // Other methods like moving, attacking, etc.
}