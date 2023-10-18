using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, IUpgradeable, IDamageable
{
    public delegate void DeathEventHandler(Unit unit);
    public event DeathEventHandler OnDeath;

    [SerializeField]
    private Enemy target;
    public Enemy Target { get { return target; } private set { target = value; } }

    [SerializeField]
    private float health;
    public float Health { get { return health; } set { health = value; } }

    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    [SerializeField]
    private string unitType;
    public string UnitType { get { return unitType; } set { unitType = value; } }

    private void Update()
    {
        if (Target == null || !IsTargetInRange())
        {
            AcquireTarget();
        }
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // Set the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, AttackRange);  // Draw a wireframe sphere with a radius equal to the attack range
    }


    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (Target != null && IsTargetInRange())
            {
                Attack(Target);
                yield return new WaitForSeconds(1f / AttackSpeed);  // Wait for the next attack based on attack speed
            }
            else
            {
                yield return null;  // Wait for the next frame if there's no target or target is out of range
            }
        }
    }

    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, Target.transform.position) <= AttackRange;
    }

    private void AcquireTarget()
    {
        // Logic to find and set the closest enemy within attack range as the target.
        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= AttackRange)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        Target = closestEnemy;
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

    public void Upgrade()
    {
        // Implement upgrade logic, for this example simply increase damage
        Damage += 10;
    }
}
