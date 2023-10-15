using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthComponentTests
{
    [Test]
    public void HealthComponent_TakeDamage_DecreasesHealth()
    {
        // Arrange
        HealthComponent health = new HealthComponent(100, 100);

        // Act
        health.TakeDamage(10);

        // Assert
        Assert.AreEqual(90, health.CurrentHealth);
    }
}
