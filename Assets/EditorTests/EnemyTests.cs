using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class EnemyTests
{
    private Enemy enemy;
    private Unit unit;
    private DeathEventHandlerTest handler;


    [SetUp]
    public void SetUp()
    {
        // Assume Enemy and Unit are MonoBehaviour, and need a GameObject to be attached to.
        GameObject enemyGameObject = new GameObject();
        enemy = enemyGameObject.AddComponent<Enemy>();
        handler = new DeathEventHandlerTest();

        GameObject unitGameObject = new GameObject();
        unit = unitGameObject.AddComponent<Unit>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemy.gameObject);
        Object.DestroyImmediate(unit.gameObject);
    }

    [Test]
    public void TakeDamage_ReducesHealthByCorrectAmount()
    {
        enemy.Health = 100;
        enemy.TakeDamage(10);
        Assert.AreEqual(90, enemy.Health);
    }

    [Test]
    public void Die_IsCalledWhenHealthIsZero()
    {
        enemy.Health = 0;
        bool isDead = enemy.Die();
        Assert.IsTrue(isDead);
    }

    [Test]
    public void Attack_DealsCorrectDamageToUnit()
    {
        enemy.Damage = 10;
        unit.Health = 100;
        enemy.Attack(unit);
        Assert.AreEqual(90, unit.Health);
    }

    [Test]
    public void Position_SetAndGet()
    {
        Vector3 newPosition = new Vector3(1, 2, 3);
        enemy.Position = newPosition;
        Assert.AreEqual(newPosition, enemy.Position);
    }

    [Test]
    public void Die_HealthIsZero_OnDeathEventIsFired()
    {
        // Arrange
        enemy.OnDeath += handler.HandleDeath;
        enemy.Health = 0;

        // Act
        enemy.Die();

        // Assert
        Assert.IsTrue(handler.EventWasFired);
    }

    [Test]
    public void Die_HealthIsGreaterThanZero_OnDeathEventIsNotFired()
    {
        // Arrange
        enemy.OnDeath += handler.HandleDeath;
        enemy.Health = 10;

        // Act
        enemy.Die();

        // Assert
        Assert.IsFalse(handler.EventWasFired);
    }

    private class DeathEventHandlerTest
    {
        public bool EventWasFired { get; private set; } = false;

        public void HandleDeath(Enemy enemy)
        {
            EventWasFired = true;
        }
    }
}
