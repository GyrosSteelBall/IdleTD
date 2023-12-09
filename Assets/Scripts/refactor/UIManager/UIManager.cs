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

    // Define an event that others can subscribe to
    public event Action OnStartWaveButtonClicked;

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
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<LivesManagerLivesUpdatedEvent>(OnLivesUpdated);
        EventBus.Instance.Unsubscribe<WaveManagerWaveStartedEvent>(HandleWaveStarted);
    }

    private void OnLivesUpdated(LivesManagerLivesUpdatedEvent inputEvent)
    {
        livesText.text = "Lives: " + inputEvent.Lives;
    }

    private void HandleWaveStarted(WaveManagerWaveStartedEvent inputEvent)
    {
        startWaveButton.interactable = false;
    }

    private void StartWaveButtonClicked()
    {
        Debug.Log("Start Wave button clicked!");

        // Raise the event
        OnStartWaveButtonClicked?.Invoke();
    }

    // Rest of your UIManager code...
}
