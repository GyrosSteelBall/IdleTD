using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject projectilePrefab;
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


    // Add a Unity event for detecting when the GameObject is clicked. This has to be called in Awake or Start.
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>(); // Adding a 2D collider for click detection
        rangeCollider = gameObject.AddComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true; // Make sure it won't interfere with physics
        rangeCollider.radius = attackRange;
        rangeCollider.enabled = false; // Disable it initially
    }

    private void OnMouseDown()
    {
        SelectUnit();
    }

    private void SelectUnit()
    {
        UIManager.instance.SetSelectedUnit(this);
        UIManager.instance.UpdateUIForSelection(this);
        ShowRange(true);
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

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1 / attackSpeed)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        Enemy target = FindTarget();
        if (target != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.Initialize(target, GetCurrentDamage());
        }
    }

    Enemy FindTarget()
    {
        // Getting all colliders within the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Enemy closestEnemy = null;
        float closestDistance = attackRange;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
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
        if (upgradeLevel < maxUpgradeLevel && GameManager.instance.gold >= upgradeCost)
        {
            GameManager.instance.gold -= upgradeCost;
            upgradeLevel++;
            ApplyUpgrade();
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
        if (GameManager.instance.gold >= upgradeCost && upgradeLevel < maxUpgradeLevel)
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
}
