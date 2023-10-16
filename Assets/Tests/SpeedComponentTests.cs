using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

[TestFixture]
public class SpeedComponentTests : MonoBehaviour
{
    private GameObject gameObject;
    private SpeedComponent speedComponent;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        speedComponent = gameObject.AddComponent<SpeedComponent>();
    }

    [TearDown]
    public void TearDown()
    {
        DestroyImmediate(gameObject);
    }

    [Test]
    public void MovementSpeed_Is_Set_Correctly()
    {
        speedComponent.SetMovementSpeed(1);
        Assert.AreEqual(1, speedComponent.GetMovementSpeed());
    }
}
