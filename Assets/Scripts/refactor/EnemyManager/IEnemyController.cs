using System;
using UnityEngine;

public interface IEnemyController
{
    void Move(Vector3 direction);
    public event Action OnDeathEnemyController;
    // void Attack();
    // other methods...
}