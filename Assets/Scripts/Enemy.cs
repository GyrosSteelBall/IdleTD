using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float Health { get; set; }
    public float Damage { get; set; }
    public float MovementSpeed { get; set; }
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, MovementSpeed * Time.deltaTime);
    }

    public void Attack(Unit target)
    {
        target.TakeDamage(Damage);
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(0, Health - damage);
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
}
