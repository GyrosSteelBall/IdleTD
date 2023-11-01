using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int lives;
    public Wave[] waves;
    private int currentWaveIndex = 0;

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
    }
}
