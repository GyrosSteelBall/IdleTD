using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetingPriority
{
    First,
    Last,
    Closest,
    Strongest,
    Weakest,
    // ... add other targeting priorities here
}

public class Unit : MonoBehaviour
{
    [Header("Unit Configuration")]
    [SerializeField] private GameObject projectilePrefab;
    public float attackSpeed;
    public float attackRange;
    private CircleCollider2D rangeCollider;
    private float attackTimer = 0f;
    private int upgradeLevel = 0; // The current upgrade level of the unit
    public int upgradeCost;
    public int maxUpgradeLevel;
    public float baseDamage; // Base damage for level 0
    public float damageIncreasePerLevel; // Damage increase per upgrade level
    public GameObject rangeIndicator;
    private Animator animator;
    public TargetingPriority targetingPriority = TargetingPriority.First;
    [SerializeField] public Sprite icon;
    private bool isAbleToAttack = true;
    public int placementCost;


    // Add a Unity event for detecting when the GameObject is clicked. This has to be called in Awake or Start.
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>(); // Adding a 2D collider for click detection
        rangeCollider = gameObject.AddComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true; // Make sure it won't interfere with physics
        rangeCollider.radius = attackRange;
        rangeCollider.enabled = false; // Disable it initially
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        SelectUnit();
    }

    private void SelectUnit()
    {
        UIManager.Instance.SetSelectedUnit(this);
        UIManager.Instance.UpdateUIForSelection(this);
        ShowRange(true);
    }

    // Call this method to enable/disable attacking
    public void EnableAttacking(bool enable)
    {
        isAbleToAttack = enable;
        if (!enable)
        {
            CancelInvoke(nameof(AttackRoutine)); // Stop attack routine when not able to attack
        }
        else if (!IsInvoking(nameof(AttackRoutine)))
        {
            InvokeRepeating(nameof(AttackRoutine), 0f, 1 / attackSpeed); // Resume attack routine
        }
    }

    public void ShowRange(bool show)
    {
        if (show)
        {
            // Scale the rangeIndicator radius to match the attackRange.
            // Assumes that the radius indicator is set up to be correct for a radius of 1 unit.
            rangeIndicator.transform.localScale = Vector3.one * attackRange * 2; // Multiply by 2 for diameter
            rangeIndicator.SetActive(true);
        }
        else
        {
            rangeIndicator.SetActive(false);
        }
    }

    private void AttackRoutine()
    {
        // Your existing attack logic, potentially moved from Update
        // Make sure you still check `isAbleToAttack` before actually attacking
        if (!isAbleToAttack)
            return;

        Enemy target = FindTarget();
        if (target != null)
        {
            FaceTarget(target);
            Attack(target);
        }
    }

    void Update()
    {
        // attackTimer += Time.deltaTime;
        // if (attackTimer >= 1 / attackSpeed)
        // {
        //     // Before attacking, ensure there is a target to face towards
        //     Enemy target = FindTarget();
        //     if (target != null)
        //     {
        //         FaceTarget(target);
        //         Attack(target);
        //         attackTimer = 0f;
        //     }
        // }
    }

    void Attack(Enemy target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.Initialize(target, GetCurrentDamage());

        // Trigger the attack animation only if a projectile is being shot
        animator.SetTrigger("Attack");
    }

    // Face towards the target by flipping the sprite on the X axis
    void FaceTarget(Enemy target)
    {
        if (target != null)
        {
            Vector3 toTarget = target.transform.position - transform.position;
            if (toTarget.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (toTarget.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    Enemy FindTarget()
    {
        // Getting all colliders within the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        Enemy target = null;

        switch (targetingPriority)
        {
            case TargetingPriority.First:
                target = FindFirst(colliders);
                break;
            case TargetingPriority.Last:
                target = FindLast(colliders);
                break;
            case TargetingPriority.Closest:
                target = FindClosest(colliders);
                break;
                // Add cases for other priorities using similar methods
        }

        return target;
    }

    Enemy FindFirst(Collider2D[] colliders)
    {
        // Assuming enemies have a 'distance along path' value
        Enemy firstEnemy = null;
        float maxDistance = float.MinValue;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && enemy.DistanceAlongPath > maxDistance && !enemy.IsDead)
            {
                maxDistance = enemy.DistanceAlongPath;
                firstEnemy = enemy;
            }
        }

        return firstEnemy;
    }

    Enemy FindLast(Collider2D[] colliders)
    {
        // Assuming enemies have a 'distance along path' value
        Enemy lastEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && enemy.DistanceAlongPath < minDistance && !enemy.IsDead)
            {
                minDistance = enemy.DistanceAlongPath;
                lastEnemy = enemy;
            }
        }

        return lastEnemy;
    }

    Enemy FindClosest(Collider2D[] colliders)
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !enemy.IsDead)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    public void UpgradeUnit()
    {
        if (upgradeLevel < maxUpgradeLevel && GameManager.Instance.Gold >= upgradeCost)
        {
            if (GameManager.Instance.SpendGold(upgradeCost))
            {
                upgradeLevel++;
                ApplyUpgrade();
            }
        }
    }

    // Call this method to calculate the current damage based on the upgrade level
    private float GetCurrentDamage()
    {
        return baseDamage + (upgradeLevel * damageIncreasePerLevel);
    }

    private void ApplyUpgrade()
    {
        // Upgrade logic, like increasing damage or attack speed
        attackSpeed += 0.5f; // For example purposes
                             // Possible further effects or animations for the upgrade
    }

    public bool CanUpgrade()
    {
        if (GameManager.Instance.Gold >= upgradeCost && upgradeLevel < maxUpgradeLevel)
        {
            return true;
        }

        return false;
    }

    public int GetUpgradeCost()
    {
        // Return current upgrade cost based on the level or some other logic.
        return upgradeCost; // Assuming you have a field named upgradeCost.
    }

    private void Idle() // You'll need to call this when the unit is not attacking
    {
        // Trigger the idle animation
        animator.SetTrigger("Idle");
    }
}
