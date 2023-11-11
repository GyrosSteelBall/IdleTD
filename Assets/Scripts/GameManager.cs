using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Static instance of GameManager which allows it to be accessed by any other script.

    public int gold;
    public int lives;
    public Wave[] waves;
    private int currentWaveIndex = 0;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // if not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].StartSpawning();
        }
    }

    public void EndWave()
    {
        // Logic for ending the wave, like giving rewards
        currentWaveIndex++;
        // Check if there are more waves to start
        if (currentWaveIndex < waves.Length)
        {
            StartWave();
        }
    }
}