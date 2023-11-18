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
    [SerializeField] private float baseDamage;
    [SerializeField] private GameObject rangeIndicator;
    private Animator animator;
    [SerializeField] private TargetingPriority targetingPriority = TargetingPriority.First;
    [SerializeField] private Sprite icon;
    private bool isAbleToAttack = true;
    [SerializeField] private int placementCost;

    public int PlacementCost => placementCost;
    public Sprite Icon => icon;
    [Header("Upgrade Paths")]
    // This list will now reference the UpgradePathSO scriptable objects.
    [SerializeField] private List<UpgradePathSO> upgradePaths;
    // Keep track of the upgrade level for each path.
    private List<int> upgradeLevelsPerPath = new List<int>();
    public List<UpgradePathSO> UpgradePaths => upgradePaths; // Read-only property to access upgrade paths.
    public List<int> UpgradeLevelsPerPath => upgradeLevelsPerPath; // Read-only property to access current upgrade levels.
    private float damage;
    // Expose the necessary fields through public properties or methods.
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = Mathf.Max(0, value); // Ensure attack speed doesn't go negative.
    }

    public float AttackRange
    {
        get => attackRange;
        set
        {
            attackRange = Mathf.Max(0, value);
            rangeCollider.radius = attackRange;
            RedrawRangeIndicator();  // Update the range indicator visual when the range changes.
        }
    }

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0, value); // Ensure damage doesn't go negative.
    }


    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true;
        rangeCollider.radius = attackRange;
        rangeCollider.enabled = false;
        animator = GetComponent<Animator>();
        damage = baseDamage;
    }

    private void Start()
    {
        foreach (var path in upgradePaths)
        {
            upgradeLevelsPerPath.Add(0);  // Start with 0 upgrade levels per path.
        }
    }

    // Method to apply an upgrade from a specific path.
    public void ApplyUpgrade(int pathIndex)
    {
        if (pathIndex < upgradePaths.Count)
        {
            UpgradePathSO path = upgradePaths[pathIndex];
            int stepIndex = upgradeLevelsPerPath[pathIndex];
            if (stepIndex < path.upgradeSteps.Count && GameManager.Instance.CanSpendGold(path.upgradeSteps[stepIndex].cost))
            {
                UpgradeEffect upgradeEffect = path.upgradeSteps[stepIndex];
                upgradeEffect.ApplyEffect(this);
                GameManager.Instance.SpendGold(upgradeEffect.cost);
                upgradeLevelsPerPath[pathIndex]++;
            }
        }
    }

    private void RedrawRangeIndicator()
    {
        if (rangeIndicator != null)
        {
            // Assuming rangeIndicator is a GameObject with a scale that represents the attack range visual.
            rangeIndicator.transform.localScale = Vector3.one * attackRange * 2f;
        }
        // You may want to activate it momentarily to give visual feedback that the upgrade has been applied.
        ShowRange(false);
        ShowRange(true);
    }

    // Method to check if an upgrade is available for a specific path.
    public bool IsUpgradeAvailable(int pathIndex)
    {
        if (pathIndex < upgradePaths.Count)
        {
            int stepIndex = upgradeLevelsPerPath[pathIndex];
            if (stepIndex < upgradePaths[pathIndex].upgradeSteps.Count)
            {
                UpgradeEffect effect = upgradePaths[pathIndex].upgradeSteps[stepIndex];
                return GameManager.Instance.CanSpendGold(effect.cost);
            }
        }
        return false;
    }

    // Placeholder for increasing the Unit's damage.
    // This should be called by specific UpgradeEffects, not directly.
    public void IncreaseDamage(float amount)
    {
        baseDamage += amount;
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

    private float GetCurrentDamage()
    {
        return damage;
    }

    private void Idle() // Call this when the unit is not attacking
    {
        animator.SetTrigger("Idle");
    }
}
