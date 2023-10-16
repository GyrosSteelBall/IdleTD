using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DefenseComponentTests
{
    private DefenseComponent defense;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        defense = gameObject.AddComponent<DefenseComponent>();
        defense.SetArmor(50);
        defense.SetMagicResistance(50);
        defense.SetDamageReduction(10);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(defense.gameObject);
    }

    [Test]
    public void DefenseComponent_SetArmor_UpdatesArmor()
    {
        // Act
        defense.SetArmor(60);

        // Assert
        Assert.AreEqual(60, defense.GetArmor());
    }

    [Test]
    public void DefenseComponent_SetMagicResistance_UpdatesMagicResistance()
    {
        // Act
        defense.SetMagicResistance(60);

        // Assert
        Assert.AreEqual(60, defense.GetMagicResistance());
    }

    [Test]
    public void DefenseComponent_SetDamageReduction_UpdatesDamageReduction()
    {
        // Act
        defense.SetDamageReduction(20);

        // Assert
        Assert.AreEqual(20, defense.GetDamageReduction());
    }

    [Test]
    public void DefenseComponent_CalculatePhysicalDamageReduction_ReturnsCorrectValue()
    {
        // Arrange
        int incomingDamage = 100;

        // Act
        int reducedDamage = defense.CalculatePhysicalDamageTaken(incomingDamage);

        // Assert
        Assert.AreEqual(60, reducedDamage);  // assuming a simple formula for illustration
    }

    [Test]
    public void DefenseComponent_CalculateMagicDamageReduction_ReturnsCorrectValue()
    {
        // Arrange
        int incomingDamage = 100;

        // Act
        int reducedDamage = defense.CalculateMagicDamageTaken(incomingDamage);

        // Assert
        Assert.AreEqual(60, reducedDamage);  // assuming a simple formula for illustration
    }

    [Test]
    public void DefenseComponent_Armor_NeverNegative()
    {
        // Act
        defense.SetArmor(-10);

        // Assert
        Assert.AreEqual(0, defense.GetArmor());  // assuming armor value is clamped to 0
    }

    [Test]
    public void DefenseComponent_MagicResistance_NeverNegative()
    {
        // Act
        defense.SetMagicResistance(-10);

        // Assert
        Assert.AreEqual(0, defense.GetMagicResistance());  // assuming magic resistance value is clamped to 0
    }

    [Test]
    public void DefenseComponent_DamageReduction_NeverNegative()
    {
        // Act
        defense.SetDamageReduction(-10);

        // Assert
        Assert.AreEqual(0, defense.GetDamageReduction());  // assuming damage reduction value is clamped to 0
    }

    [Test]
    public void DefenseComponent_OnArmorChanged_EventIsTriggered()
    {
        // Arrange
        int oldArmor = 0;
        int newArmor = 0;
        defense.OnArmorChanged += (o, n) => { oldArmor = o; newArmor = n; };

        // Act
        defense.SetArmor(60);  // Triggering the event

        // Assert
        Assert.AreEqual(50, oldArmor);
        Assert.AreEqual(60, newArmor);
    }

    [Test]
    public void DefenseComponent_OnMagicResistanceChanged_EventIsTriggered()
    {
        // Arrange
        int oldMagicResistance = 0;
        int newMagicResistance = 0;
        defense.OnMagicResistanceChanged += (o, n) => { oldMagicResistance = o; newMagicResistance = n; };

        // Act
        defense.SetMagicResistance(60);  // Triggering the event

        // Assert
        Assert.AreEqual(50, oldMagicResistance);
        Assert.AreEqual(60, newMagicResistance);
    }

    [Test]
    public void DefenseComponent_OnDamageReductionChanged_EventIsTriggered()
    {
        // Arrange
        float oldDamageReduction = 0;
        float newDamageReduction = 0;
        defense.OnDamageReductionChanged += (o, n) => { oldDamageReduction = o; newDamageReduction = n; };

        // Act
        defense.SetDamageReduction(20);  // Triggering the event

        // Assert
        Assert.AreEqual(10, oldDamageReduction);
        Assert.AreEqual(20, newDamageReduction);
    }

}
