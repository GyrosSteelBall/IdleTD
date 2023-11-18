using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UpgradePath", menuName = "UpgradeSystem/UpgradePath")]
public class UpgradePathSO : ScriptableObject
{
    public List<UpgradeEffect> upgradeSteps;
}