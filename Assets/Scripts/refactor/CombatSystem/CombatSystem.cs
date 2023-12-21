using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{
    void OnEnable()
    {
        EventBus.Instance.Subscribe<UnitControllerAttackEvent>(HandleUnitAttack);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UnitControllerAttackEvent>(HandleUnitAttack);
    }

    private void HandleUnitAttack(UnitControllerAttackEvent attackEvent)
    {
        // Calculate actual damage here. For now, let's just pass through the raw damage.
        float actualDamage = attackEvent.RawDamage;

        EventBus.Instance.Publish(new CombatSystemApplyDamageToEnemyEvent(attackEvent.Target, actualDamage));
    }
}