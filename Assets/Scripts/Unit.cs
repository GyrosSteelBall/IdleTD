using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUpgradeable, IDamageable
{
    public float Health { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    public string UnitType { get; set; }

    public void Attack(Enemy target)
    {
        // Assuming a simple direct damage mechanism for this example
        target.TakeDamage(Damage);
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(0, Health - damage);  // Ensure Health does not go below zero
        if (Health <= 0)
        {
            Die();
        }
    }

    public bool Die()
    {
        // Implement death handling, for this example return true when Die is called, 
        // but only if Health is zero or less
        return Health <= 0;
    }

    public void Upgrade()
    {
        // Implement upgrade logic, for this example simply increase damage
        Damage += 10;
    }
}
