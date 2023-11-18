using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeEffects/AttackSpeedIncrease")]
public class AttackSpeedIncreaseUpgradeEffect : UpgradeEffect
{
    public float speedMultiplier = 1.2f; //20% increased attack speed

    public override void ApplyEffect(Unit unit)
    {
        unit.AttackSpeed *= speedMultiplier;
    }
}