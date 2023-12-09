using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Vector3 spawnPoint;
    private int currentWaveIndex = -1;
    public event Action<EnemyData, Vector3> OnSpawnEnemyRequest;

    protected override void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<UIManagerStartWaveButtonClickedEvent>(HandleStartWaveButtonClicked);
        EventBus.Instance.Subscribe<EnemyManagerAllEnemiesDefeatedEvent>(OnAllEnemiesCleared);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIManagerStartWaveButtonClickedEvent>(HandleStartWaveButtonClicked);
        EventBus.Instance.Unsubscribe<EnemyManagerAllEnemiesDefeatedEvent>(OnAllEnemiesCleared);
    }

    private void HandleStartWaveButtonClicked(UIManagerStartWaveButtonClickedEvent inputEvent)
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        Debug.Log("Starting Wave in WaveManager");
        currentWaveIndex++;
        if (currentWaveIndex < waves.Count)
        {
            var wave = waves[currentWaveIndex];
            EventBus.Instance.Publish(new WaveManagerWaveStartedEvent(currentWaveIndex));
            StartCoroutine(SpawnWave(wave));
        }
        else
        {
            // Handle end of all waves
            GameManager.Instance.ChangeState(new VictoryState());
        }
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
    }

    private Vector3 GetSpawnPosition()
    {
        // Get the position where enemies spawn, e.g., from a dedicated spawn point transform
        return spawnPoint;
    }

    private void OnAllEnemiesCleared(EnemyManagerAllEnemiesDefeatedEvent inputEvent)
    {
        EventBus.Instance.Publish(new WaveManagerWaveCompletedEvent());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // ... Other WaveManager functionality ...
}