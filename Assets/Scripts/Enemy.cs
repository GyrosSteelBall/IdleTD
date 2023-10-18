using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public delegate void DeathEventHandler(Enemy enemy);
    public event DeathEventHandler OnDeath;
    [SerializeField]
    private float health;
    public float Health { get { return health; } set { health = value; } }

    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public bool Die()
    {
        bool isDead = Health <= 0;
        if (isDead)
        {
            OnDeath?.Invoke(this);
        }
        return isDead;
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
}
