using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

[TestFixture]
public class AttackComponentTests : MonoBehaviour
{
    private GameObject gameObject;
    private AttackComponent attackComponent;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        attackComponent = gameObject.AddComponent<AttackComponent>();
    }

    [TearDown]
    public void TearDown()
    {
        DestroyImmediate(gameObject);
    }

    [Test]
    public void AttackDamage_Is_Set_Correctly()
    {
        attackComponent.SetAttackDamage(10);
        Assert.AreEqual(10, attackComponent.GetAttackDamage());
    }

    [Test]
    public void AttackSpeed_Is_Set_Correctly()
    {
        attackComponent.SetAttackSpeed(1.5f);
        Assert.AreEqual(1.5f, attackComponent.GetAttackSpeed());
    }

    [Test]
    public void AttackRange_Is_Set_Correctly()
    {
        attackComponent.SetAttackRange(5);
        Assert.AreEqual(5, attackComponent.GetAttackRange());
    }

    [Test]
    public void AttackType_Is_Set_Correctly()
    {
        attackComponent.SetAttackType("aoe");
        Assert.AreEqual("aoe", attackComponent.GetAttackType());
    }

    [Test]
    public void CriticalChance_Is_Set_Correctly()
    {
        attackComponent.SetCriticalChance(0.25f);
        Assert.AreEqual(0.25f, attackComponent.GetCriticalChance());
    }

    [Test]
    public void CriticalDamageMultiplier_Is_Set_Correctly()
    {
        attackComponent.SetCriticalDamageMultiplier(2.25f);
        Assert.AreEqual(2.25f, attackComponent.getCriticalDamageMultiplier());
    }

    [Test]
    [TestCase("single", 10, 1.0f, 5, 0.1f)]
    [TestCase("aoe", 20, 1.5f, 10, 0.2f)]
    [TestCase("chaining", 15, 1.2f, 7, 0.15f)]
    public void AttackComponent_Is_Configured_Correctly(
        string type, int damage, float speed, int range, float critChance)
    {
        attackComponent.ConfigureAttack(type, damage, speed, range, critChance);
        Assert.AreEqual(type, attackComponent.GetAttackType());
        Assert.AreEqual(damage, attackComponent.GetAttackDamage());
        Assert.AreEqual(speed, attackComponent.GetAttackSpeed());
        Assert.AreEqual(range, attackComponent.GetAttackRange());
        Assert.AreEqual(critChance, attackComponent.GetCriticalChance());
    }
}
