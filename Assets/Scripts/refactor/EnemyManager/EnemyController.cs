using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyController
{
    private List<Vector3> currentPath;
    private int currentWaypointIndex = 0;
    private string lastDirection = null;
    private IEnemyState _currentState;
    //Enemy for this controller
    public Enemy ParentEnemy { get; set; }
    [SerializeField]
    private int AttackDamage;
    [SerializeField]
    private int AbilityPower;
    [SerializeField]
    private int Armor;
    [SerializeField]
    private int MagicResist;
    [SerializeField]
    private float AttackSpeed;
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private int MaxHealth;
    [SerializeField]
    private int CurrentHealth;
    [SerializeField]
    private int MaxMana;
    [SerializeField]
    private int CurrentMana;
    [SerializeField]
    private Sprite EnemySprite;
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private string EnemyName;

    void Awake()
    {
        ChangeState(new EnemyMovingState());
    }

    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<CombatSystemApplyDamageToEnemyEvent>(HandleApplyDamageToEnemyEvent);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<CombatSystemApplyDamageToEnemyEvent>(HandleApplyDamageToEnemyEvent);
    }

    public void Initialize(EnemyDataSO data)
    {
        EnemyName = data.enemyName;
        MaxHealth = data.baseMaxHealth;
        AttackDamage = data.baseAttackDamage;
        AbilityPower = data.baseAbilityPower;
        Armor = data.baseArmor;
        MagicResist = data.baseMagicResist;
        AttackSpeed = data.baseAttackSpeed;
        AttackRange = data.baseAttackRange;
        MaxMana = data.baseMaxMana;
        EnemySprite = data.enemySprite;
        MovementSpeed = data.baseMovementSpeed;
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    private void HandleApplyDamageToEnemyEvent(CombatSystemApplyDamageToEnemyEvent damageEvent)
    {
        if (damageEvent.Target == this)
        {
            TakeDamage(damageEvent.Damage);
        }
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

    void Update()
    {
        _currentState?.Update(this);
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, MovementSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        EventBus.Instance.Publish(new EnemyControllerTakeDamageEvent(this, damage));
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsUnitInFront(float detectionDistance)
    {
        // Convert lastDirection string to Vector2
        Vector2 direction = new Vector2(0, 0);
        if (lastDirection == "up")
        {
            direction = Vector2.up;
        }
        else if (lastDirection == "down")
        {
            direction = Vector2.down;
        }
        else if (lastDirection == "left")
        {
            direction = Vector2.left;
        }
        else if (lastDirection == "right")
        {
            direction = Vector2.right;
        }

        // Cast a ray in the direction the enemy is moving
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);

        // If the ray hit a Unit, return true
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Unit"))
        {
            ChangeState(new EnemyCombatState(hit.collider.gameObject.GetComponent<UnitController>()));
            return true;
        }

        // Otherwise, return false
        return false;
    }

    public void Attack(UnitController target)
    {
        EventBus.Instance.Publish(new EnemyControllerAttackEvent(this, target, AttackDamage));
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

        //Detection distance of 1 for melee blockers
        if (IsUnitInFront(0.5f))
        {
            // Stop moving if there's a Unit in front
            return;
        }

        if (currentWaypointIndex >= currentPath.Count)
        {
            EventBus.Instance.Publish(new EnemyControllerReachedFinalWaypointEvent(this));
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

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    // Other methods like moving, attacking, etc.
}