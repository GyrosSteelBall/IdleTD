using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController
{
    private EnemyData enemyData;
    private List<Vector3> currentPath;
    private int currentWaypointIndex = 0;
    private float movementSpeed = 4.0f;
    private string lastDirection = null;
    private IEnemyState _currentState;
    //Enemy for this controller
    public Enemy ParentEnemy { get; set; }

    void Awake()
    {
        ChangeState(new EnemyMovingState());
    }

    public void ChangeState(IEnemyState newState)
    {
        EventBus.Instance.Publish(new EnemyControllerChangedStateEvent(newState));
        _currentState?.OnStateExit(this);
        _currentState = newState;
        _currentState.OnStateEnter(this);
    }

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
        _currentState?.Update(this);
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

    public void MoveTowardsNextWaypoint()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            Debug.Log("No path assigned.");
            return;
        }

        if (currentWaypointIndex >= currentPath.Count)
        {
            EventBus.Instance.Publish(new EnemyControllerReachedFinalWaypointEvent(this));
            Destroy(gameObject);
            return;
        }

        Vector3 target = currentPath[currentWaypointIndex];
        Vector3 directionVector = (target - transform.position).normalized;

        string direction;
        if (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y))
        {
            direction = directionVector.x > 0 ? "right" : "left";
        }
        else
        {
            direction = directionVector.y > 0 ? "up" : "down";
        }

        if (direction != lastDirection)
        {
            EventBus.Instance.Publish(new EnemyControllerMovementDirectionChangedEvent(this, direction));
            lastDirection = direction;
        }

        MoveTowards(target);

        if (Vector3.Distance(transform.position, currentPath[currentWaypointIndex]) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }

    // Other methods like moving, attacking, etc.
}