using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Vector3 spawnPoint;
    private int currentWaveIndex = -1;
    // Define an event for signaling the beginning and completion of a wave
    public event Action<int> OnWaveStarted;
    public event Action OnWaveCompleted;
    public event Action<EnemyData, Vector3> OnSpawnEnemyRequest;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.OnStartWaveButtonClicked += StartNextWave;
    }

    public void StartNextWave()
    {
        Debug.Log("Starting Wave in WaveManager");
        OnWaveStarted?.Invoke(currentWaveIndex);
        // currentWaveIndex++;
        // if (currentWaveIndex < waves.Count)
        // {
        //     var wave = waves[currentWaveIndex];
        //     OnWaveStarted?.Invoke(currentWaveIndex);
        //     StartCoroutine(SpawnWave(wave));
        // }
        // else
        // {
        //     // Handle end of all waves
        //     GameManager.Instance.ChangeState(new VictoryState());
        // }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var spawnInfo in wave.enemySpawns)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                // Tell the EnemyManager to spawn each enemy at the appropriate location
                OnSpawnEnemyRequest?.Invoke(spawnInfo.enemyData, GetSpawnPosition());

                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
        // After spawning all enemies, subscribe to the OnAllEnemiesDefeated event of the EnemyManager to know when to complete the wave
        EnemyManager.Instance.OnAllEnemiesDefeated += OnAllEnemiesCleared;
    }

    private Vector3 GetSpawnPosition()
    {
        // Get the position where enemies spawn, e.g., from a dedicated spawn point transform
        return spawnPoint;
    }

    private void OnAllEnemiesCleared()
    {
        // Unsubscribe to the event to avoid getting called multiple times
        EnemyManager.Instance.OnAllEnemiesDefeated -= OnAllEnemiesCleared;
        OnWaveCompleted?.Invoke();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.OnStartWaveButtonClicked -= StartNextWave;
    }

    // ... Other WaveManager functionality ...
}