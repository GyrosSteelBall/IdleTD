using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CombatManagerTests : MonoBehaviour
{
    private CombatManager combatManager;
    private Unit unit;
    private Enemy enemy;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        GameObject combatManagerObject = new GameObject();
        combatManager = combatManagerObject.AddComponent<CombatManager>();

        GameObject unitObject = new GameObject();
        unit = unitObject.AddComponent<Unit>();

        GameObject enemyObject = new GameObject();
        enemy = enemyObject.AddComponent<Enemy>();

        combatManager.RegisterUnit(unit);
        combatManager.RegisterEnemy(enemy);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.DestroyImmediate(combatManager.gameObject);
        Object.DestroyImmediate(unit.gameObject);
        Object.DestroyImmediate(enemy.gameObject);

        yield return null;
    }

    [UnityTest]
    public IEnumerator HandleCombat_EnemyWithinRange_IsAttacked()
    {
        unit.Damage = 10;
        enemy.Health = 100;
        enemy.transform.position = unit.transform.position + Vector3.forward;  // Position enemy within 1 unit distance
        unit.AttackRange = 2f;  // Set unit attack range to cover the distance

        yield return ResolveCombatOverTime(duration: 2f);  // Allow up to 2 seconds for the combat to resolve

        Assert.AreEqual(90, enemy.Health);  // Verify enemy health is reduced by unit's damage
    }

    private IEnumerator ResolveCombatOverTime(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for one frame
        }
    }

}
