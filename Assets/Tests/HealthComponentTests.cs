using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthComponentTests
{
    private HealthComponent health;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        health = gameObject.AddComponent<HealthComponent>();
        health.SetMaxHealth(100);
        health.SetCurrentHealth(100);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(health.gameObject);
    }

    [Test]
    public void HealthComponent_TakeDamage_DecreasesHealth()
    {
        // Act
        health.TakeDamage(10);

        // Assert
        Assert.AreEqual(90, health.GetCurrentHealth());
    }

    [Test]
    public void HealthComponent_Heal_IncreasesHealth()
    {
        // Arrange
        health.TakeDamage(20);  // Health is now 80

        // Act
        health.Heal(10);

        // Assert
        Assert.AreEqual(90, health.GetCurrentHealth());
    }

    [Test]
    public void HealthComponent_Heal_NotExceedMaxHealth()
    {
        // Act
        health.Heal(10);

        // Assert
        Assert.AreEqual(100, health.GetCurrentHealth());
    }

    [Test]
    public void HealthComponent_SetMaxHealth_UpdatesMaxHealth()
    {
        // Act
        health.SetMaxHealth(120);

        // Assert
        Assert.AreEqual(120, health.GetMaxHealth());
    }

    [Test]
    public void HealthComponent_IsDead_ReturnsTrueWhenHealthIsZero()
    {
        // Arrange
        health.TakeDamage(100);  // Health is now 0

        // Assert
        Assert.IsTrue(health.GetIsDead());
    }

    [Test]
    public void HealthComponent_OnHealthChanged_EventIsTriggered()
    {
        // Arrange
        int oldHealth = 0;
        int newHealth = 0;
        health.OnHealthChanged += (o, n) => { oldHealth = o; newHealth = n; };

        // Act
        health.TakeDamage(10);  // Health is now 90

        // Assert
        Assert.AreEqual(100, oldHealth);
        Assert.AreEqual(90, newHealth);
    }

}
