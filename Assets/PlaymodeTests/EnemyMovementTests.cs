using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyMovementTests : MonoBehaviour
{
    private Enemy enemy;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        GameObject enemyGameObject = new GameObject();
        enemy = enemyGameObject.AddComponent<Enemy>();
        enemy.MovementSpeed = 3f;  // Matching the MovementSpeed from your script
        yield return null;  // Wait for one frame
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.DestroyImmediate(enemy.gameObject);
        yield return null;  // Wait for one frame
    }

    [UnityTest]
    public IEnumerator Move_UpdatesPositionBasedOnMovementSpeed()
    {
        Vector3 targetPosition = new Vector3(1, 2, 0);
        yield return MoveEnemyTowardsTargetPosition(targetPosition);

        Vector3 finalPosition = enemy.Position;
        Assert.AreEqual(targetPosition.x, finalPosition.x, 0.01f);
        Assert.AreEqual(targetPosition.y, finalPosition.y, 0.01f);
        Assert.AreEqual(targetPosition.z, finalPosition.z, 0.01f);
    }


    private IEnumerator MoveEnemyTowardsTargetPosition(Vector3 targetPosition)
    {
        float maxDuration = 5f;  // Maximum duration to attempt reaching the target position
        float elapsedTime = 0f;

        while (Vector3.Distance(enemy.Position, targetPosition) > 0.01f && elapsedTime < maxDuration)
        {
            enemy.Move(targetPosition);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for one frame
        }
    }
}
