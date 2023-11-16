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
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Unit : MonoBehaviour
{
    [Header("Unit Configuration")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    private CircleCollider2D rangeCollider;
    private float attackTimer = 0f;
    private int upgradeLevel = 0;
    [SerializeField] private int upgradeCost;
    [SerializeField] private int maxUpgradeLevel;
    [SerializeField] private float baseDamage;
    [SerializeField] private float damageIncreasePerLevel;
    [SerializeField] private GameObject rangeIndicator;
    private Animator animator;
    [SerializeField] private TargetingPriority targetingPriority = TargetingPriority.First;
    [SerializeField] private Sprite icon;
    private bool isAbleToAttack = true;
    [SerializeField] private int placementCost;

    public int PlacementCost => placementCost;
    public Sprite Icon => icon;

    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true;
        rangeCollider.radius = attackRange;
        rangeCollider.enabled = false;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Further initialization if needed
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

    public void EnableAttacking(bool enable)
    {
        isAbleToAttack = enable;
        if (enable)
            StartCoroutine(AttackRoutine());
        else
            StopCoroutine(AttackRoutine());
    }

    public void ShowRange(bool show)
    {
        rangeIndicator.transform.localScale = Vector3.one * attackRange * 2;
        rangeIndicator.SetActive(show);
    }

    public void ChangeRangeIndicatorColor(Color newColor)
    {
        rangeIndicator.GetComponent<SpriteRenderer>().color = newColor;
    }

    private IEnumerator AttackRoutine()
    {
        while (isAbleToAttack)
        {
            Enemy target = FindTarget();
            if (target != null)
            {
                FaceTarget(target);
                Attack(target);
            }
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }

    void Attack(Enemy target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.Initialize(target, GetCurrentDamage());
        animator.SetTrigger("Attack");
    }

    void FaceTarget(Enemy target)
    {
        if (target == null) return;
        Vector3 toTarget = target.transform.position - transform.position;
        transform.right = toTarget.x >= 0 ? Vector3.right : Vector3.left;
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
        if (CanUpgrade())
        {
            upgradeLevel++;
            ApplyUpgrade();
            GameManager.Instance.SpendGold(upgradeCost);
        }
    }

    private float GetCurrentDamage()
    {
        return baseDamage + (upgradeLevel * damageIncreasePerLevel);
    }

    private void ApplyUpgrade()
    {
        // Upgrade logic
        attackSpeed += 0.5f; // Adjustment for example purposes
    }

    public bool CanUpgrade()
    {
        return GameManager.Instance.Gold >= upgradeCost && upgradeLevel < maxUpgradeLevel;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    private void Idle() // Call this when the unit is not attacking
    {
        animator.SetTrigger("Idle");
    }
}
