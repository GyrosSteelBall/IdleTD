using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private EnemyController controller;

    public event Action OnDeathEnemy;

    // private EnemyAnimator animator;
    // private EnemyVisuals visuals;

    public void Initialize(EnemyData enemyData, Vector3 spawnPosition)
    {
        // Initialize the enemy with the provided data and position
    }

    public void Die()
    {
        // Handle death logic, such as playing animations, dropping loot, etc.
        OnDeathEnemy?.Invoke();
    }

    // Implement other IEnemy methods and any additional functionality
}
