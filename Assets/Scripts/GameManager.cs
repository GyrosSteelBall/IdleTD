using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // Using a private static variable to hold the instance.

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) // Check if instance already exists in the scene.
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null) // No instance found, create a new one.
                {
                    GameObject gm = new GameObject("GameManager");
                    _instance = gm.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    [Header("Game Configuration")]
    [SerializeField] private int startingGold = 10;
    [SerializeField] private int startingLives = 10;
    public int Gold { get; private set; } // Encapsulate fields
    public int Lives { get; private set; } // Encapsulate fields

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private int currentWaveIndex = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Gold = startingGold;
        Lives = startingLives;
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
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            StartWave();
        }
        // Otherwise, handle the end of all waves (e.g. victory screen)
    }

    public bool SpendGold(int amount)
    {
        if (Gold - amount >= 0)
        {
            Gold -= amount;
            return true;
        }
        return false; // Return a boolean to indicate if the gold was successfully spent.
    }

    public bool CanSpendGold(int amount)
    {
        if (Gold - amount >= 0)
        {
            return true;
        }
        return false;
    }

    // A method for adding gold that can be invoked by other scripts, consistent with encapsulation.
    public void AddGold(int amount)
    {
        if (amount > 0)
        {
            Gold += amount;
        }
    }

    // A method for updating lives.
    public void UpdateLives(int amount)
    {
        Lives += amount; // This allows for adding or subtracting lives.
    }
}

// The Wave class will remain unchanged for this refactor since no information is provided about it.
// You should invoke GameManager.Instance.AddGold(amount) and GameManager.Instance.UpdateLives(amount)
// in other scripts where you are modifying the gold and lives directly.