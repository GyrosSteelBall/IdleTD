using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class UnitTests
{
    private Unit unit;
    private Enemy enemy;
    private DeathEventHandlerTest handler;

    [SetUp]
    public void SetUp()
    {
        // Assume Unit and Enemy are MonoBehaviour, and need a GameObject to be attached to.
        GameObject unitGameObject = new GameObject();
        unit = unitGameObject.AddComponent<Unit>();

        GameObject enemyGameObject = new GameObject();
        enemy = enemyGameObject.AddComponent<Enemy>();

        handler = new DeathEventHandlerTest();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(unit.gameObject);
        Object.DestroyImmediate(enemy.gameObject);
    }

    [Test]
    public void TakeDamage_ReducesHealthByCorrectAmount()
    {
        unit.Health = 100;
        unit.TakeDamage(10);
        Assert.AreEqual(90, unit.Health);
    }

    [Test]
    public void Die_IsCalledWhenHealthIsZero()
    {
        unit.Health = 0;
        bool isDead = unit.Die();
        Assert.IsTrue(isDead);
    }

    [Test]
    public void Attack_DealsCorrectDamageToEnemy()
    {
        unit.Damage = 10;
        enemy.Health = 100;
        unit.Attack(enemy);
        Assert.AreEqual(90, enemy.Health);
    }

    [Test]
    public void Upgrade_IncreasesUnitStats()
    {
        unit.Damage = 10;
        unit.Upgrade();
        Assert.AreEqual(20, unit.Damage);  // Assuming Upgrade increases Damage by 10
    }

    [Test]
    public void TakeDamage_HealthCannotGoBelowZero()
    {
        unit.Health = 5;
        unit.TakeDamage(10);
        Assert.AreEqual(0, unit.Health);
    }

    [Test]
    public void Die_HealthIsZero()
    {
        unit.Health = 0;
        bool isDead = unit.Die();
        Assert.IsTrue(isDead);
    }

    [Test]
    public void Die_HealthIsGreaterThanZero()
    {
        unit.Health = 10;
        bool isDead = unit.Die();
        Assert.IsFalse(isDead);  // Assuming Die returns false when Health > 0
    }

    [Test]
    public void Position_SetAndGet()
    {
        Vector3 newPosition = new Vector3(1, 2, 3);
        unit.Position = newPosition;
        Assert.AreEqual(newPosition, unit.Position);
    }

    [Test]
    public void UnitType_SetAndGet()
    {
        string newType = "Mage";
        unit.UnitType = newType;
        Assert.AreEqual(newType, unit.UnitType);
    }

    [Test]
    public void AttackSpeed_SetAndGet()
    {
        float newAttackSpeed = 2.0f;
        unit.AttackSpeed = newAttackSpeed;
        Assert.AreEqual(newAttackSpeed, unit.AttackSpeed);
    }

    [Test]
    public void AttackRange_SetAndGet()
    {
        float newAttackRange = 5.0f;
        unit.AttackRange = newAttackRange;
        Assert.AreEqual(newAttackRange, unit.AttackRange);
    }

    [Test]
    public void Die_HealthIsZero_OnDeathEventIsFired()
    {
        // Arrange
        unit.OnDeath += handler.HandleDeath;
        unit.Health = 0;

        // Act
        unit.Die();

        // Assert
        Assert.IsTrue(handler.EventWasFired);
    }

    [Test]
    public void Die_HealthIsGreaterThanZero_OnDeathEventIsNotFired()
    {
        // Arrange
        unit.OnDeath += handler.HandleDeath;
        unit.Health = 10;

        // Act
        unit.Die();

        // Assert
        Assert.IsFalse(handler.EventWasFired);
    }

    private class DeathEventHandlerTest
    {
        public bool EventWasFired { get; private set; } = false;

        public void HandleDeath(Unit unit)  // Match the signature for Unit's OnDeath event
        {
            EventWasFired = true;
        }
    }
}
