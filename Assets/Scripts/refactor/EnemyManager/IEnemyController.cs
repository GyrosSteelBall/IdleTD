using System;
using UnityEngine;

public interface IEnemyController
{
    public void MoveTowardsNextWaypoint();
    public event Action OnDeathEnemyController;
    // void Attack();
    // other methods...
}