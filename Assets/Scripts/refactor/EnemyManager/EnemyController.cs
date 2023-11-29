using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController
{
    private EnemyData enemyData;
    public event Action OnDeath;
    public event Action OnDeathEnemyController;
    private List<Vector3> currentPath;
    private int currentWaypointIndex = 0;
    private float movementSpeed = 5.0f;

    public void SetPath(List<Vector3> path)
    {
        currentPath = path;
    }

    public void Initialize(EnemyData data)
    {
        enemyData = data;
        // Initialize logic-specific properties based on EnemyData
    }

    void Update()
    {
        if (currentPath != null && currentWaypointIndex < currentPath.Count)
        {
            Vector3 targetPosition = currentPath[currentWaypointIndex];
            MoveTowards(targetPosition);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // Threshold to consider as reached
            {
                currentWaypointIndex++;
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        // Reduce health and possibly trigger death
    }

    public void Die()
    {
        EventBus.Instance.Publish(new EnemyControllerEnemyDeathEvent(this));
    }

    public void Move(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    // Other methods like moving, attacking, etc.
}