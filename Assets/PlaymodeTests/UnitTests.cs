using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitPlayModeTests : MonoBehaviour
{
    private Unit unit;
    private Enemy enemy1;
    private Enemy enemy2;
    private GameObject unitGameObject;
    private GameObject enemy1GameObject;
    private GameObject enemy2GameObject;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        unitGameObject = new GameObject();
        unit = unitGameObject.AddComponent<Unit>();

        enemy1GameObject = new GameObject();
        enemy1 = enemy1GameObject.AddComponent<Enemy>();

        enemy2GameObject = new GameObject();
        enemy2 = enemy2GameObject.AddComponent<Enemy>();

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.DestroyImmediate(unitGameObject);
        Object.DestroyImmediate(enemy1GameObject);
        Object.DestroyImmediate(enemy2GameObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Attack_ReducesEnemyHealth()
    {
        unit.Damage = 10;
        enemy1.Health = 100;
        unit.Attack(enemy1);
        yield return null;
        Assert.AreEqual(90, enemy1.Health);
    }

    [UnityTest]
    public IEnumerator Unit_SelectsTargetWithinRange()
    {
        enemy1.transform.position = unit.transform.position + Vector3.forward;  // Within 1 unit distance
        unit.AttackRange = 2f;
        yield return null;

        // Assuming you've implemented a method to get the current target
        Enemy currentTarget = unit.Target;
        Assert.AreEqual(enemy1.transform.position, currentTarget.transform.position);
    }


    [UnityTest]
    public IEnumerator Unit_UpdatesTargetWhenCurrentTargetLeavesRange()
    {
        enemy1.transform.position = unit.transform.position + Vector3.forward;  // Within 1 unit distance
        enemy2.transform.position = unit.transform.position + Vector3.back * 3; // Outside of the range initially
        unit.AttackRange = 2f;

        yield return null;
        Assert.AreEqual(enemy1, unit.Target);  // Initially, enemy1 is the target

        // Now move enemy1 out of range and enemy2 within range
        enemy1.transform.position += Vector3.back * 3;
        enemy2.transform.position += Vector3.forward * 3;

        yield return new WaitForSeconds(0.1f); // Give some time for the unit to update its target

        Assert.AreEqual(enemy2, unit.Target); // Now, enemy2 should be the target
    }
}
