using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeEffects/RangeIncrease")]
public class RangeIncreaseUpgradeEffect : UpgradeEffect
{
    public float rangeBonus = 2f;

    public override void ApplyEffect(Unit unit)
    {
        unit.AttackRange += rangeBonus;
    }
}