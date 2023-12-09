using UnityEngine;
using UnityEngine.UI;
using System;  // Required for defining events
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Button startWaveButton; // Serialized field for the Start Wave button
    [SerializeField]
    private TextMeshProUGUI livesText; // Serialized field for the Lives text
    [SerializeField]
    private TextMeshProUGUI waveNumberText; // Serialized field for the Wave Number text

    protected override void Awake()
    {
        base.Awake();
        if (startWaveButton == null)
        {
            Debug.LogError("Start Wave button is not assigned in the Inspector!");
        }
        else
        {
            Debug.Log("here");
            startWaveButton.onClick.AddListener(StartWaveButtonClicked);
        }
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<LivesManagerLivesUpdatedEvent>(OnLivesUpdated);
        EventBus.Instance.Subscribe<WaveManagerWaveStartedEvent>(HandleWaveStarted);
        EventBus.Instance.Subscribe<WaveManagerWaveCompletedEvent>(HandleWaveCompleted);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<LivesManagerLivesUpdatedEvent>(OnLivesUpdated);
        EventBus.Instance.Unsubscribe<WaveManagerWaveStartedEvent>(HandleWaveStarted);
        EventBus.Instance.Unsubscribe<WaveManagerWaveCompletedEvent>(HandleWaveCompleted);
    }

    private void OnLivesUpdated(LivesManagerLivesUpdatedEvent inputEvent)
    {
        livesText.text = "Lives: " + inputEvent.Lives;
    }

    private void HandleWaveStarted(WaveManagerWaveStartedEvent inputEvent)
    {
        Debug.Log("Wave started!" + inputEvent.WaveNumber);
        startWaveButton.interactable = false;
        waveNumberText.text = $"Wave: {inputEvent.WaveNumber + 1}"; // Update the wave number text
    }

    private void StartWaveButtonClicked()
    {
        Debug.Log("Start Wave button clicked!");
        EventBus.Instance.Publish(new UIManagerStartWaveButtonClickedEvent());
    }


    private void HandleWaveCompleted(WaveManagerWaveCompletedEvent inputEvent)
    {
        startWaveButton.interactable = true;
    }
    // Rest of your UIManager code...
}
