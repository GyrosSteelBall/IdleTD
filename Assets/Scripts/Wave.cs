using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int numberOfEnemies;
    public Vector3 startPosition;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
