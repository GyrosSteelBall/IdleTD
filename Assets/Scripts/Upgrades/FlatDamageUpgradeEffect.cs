using UnityEngine;

[CreateAssetMenu(fileName = "FlatDamageUpgradeEffect", menuName = "UpgradeEffects/FlatDamage")]
public class FlatDamageUpgradeEffect : UpgradeEffect
{
    public float damageIncreaseAmount;

    public override void ApplyEffect(Unit unit)
    {
        unit.Damage += damageIncreaseAmount;
    }
}