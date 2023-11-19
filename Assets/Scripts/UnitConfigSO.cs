using System.Collections.Generic;
using UnityEngine;

public enum TargetingPriority
{
    First,
    Last,
    Closest,
    Strongest,
    Weakest,
    // ... add other targeting priorities here
}

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Units/Unit Configuration")]
public class UnitConfigSO : ScriptableObject
{
    public GameObject projectilePrefab;
    public float baseAttackSpeed;
    public float baseAttackRange;
    public float baseDamage;
    public Sprite icon;
    public int placementCost;
    public TargetingPriority targetingPriority = TargetingPriority.First;
    public List<UpgradePathSO> upgradePaths;
}