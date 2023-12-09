using UnityEngine;

public class WaveManagerWaveStartedEvent
{
    public int WaveNumber { get; private set; }

    public WaveManagerWaveStartedEvent(int waveNumber)
    {
        WaveNumber = waveNumber;
    }
}
