using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class CombatManagerTests
{
    private CombatManager combatManager;
    private Unit unit;
    private Enemy enemy;

    [SetUp]
    public void SetUp()
    {
        GameObject combatManagerObject = new GameObject();
        combatManager = combatManagerObject.AddComponent<CombatManager>();

        GameObject unitObject = new GameObject();
        unit = unitObject.AddComponent<Unit>();

        GameObject enemyObject = new GameObject();
        enemy = enemyObject.AddComponent<Enemy>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(combatManager.gameObject);
        Object.DestroyImmediate(unit.gameObject);
        Object.DestroyImmediate(enemy.gameObject);
    }

    [Test]
    public void RegisterAndDeregisterUnit()
    {
        combatManager.RegisterUnit(unit);
        Assert.Contains(unit, combatManager.Units);

        combatManager.DeregisterUnit(unit);
        Assert.IsFalse(combatManager.Units.Contains(unit));
    }

    [Test]
    public void RegisterAndDeregisterEnemy()
    {
        combatManager.RegisterEnemy(enemy);
        Assert.Contains(enemy, combatManager.Enemies);

        combatManager.DeregisterEnemy(enemy);
        Assert.IsFalse(combatManager.Enemies.Contains(enemy));
    }
}
