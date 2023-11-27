using System;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private int _gold;

    public int Gold
    {
        get => _gold;
        private set
        {
            if (_gold != value)
            {
                _gold = value;
                OnGoldChanged?.Invoke(_gold);
            }
        }
    }

    // Event to notify when the gold resources change
    public event Action<int> OnGoldChanged;

    public void AddGold(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Attempt to add negative gold amount.");
            return; // Optionally handle invalid amounts
        }
        Gold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Attempt to spend negative or zero gold amount.");
            return false; // Optionally handle invalid amounts
        }

        if (_gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough gold to spend.");
            return false;
        }
    }

    protected override void OnDestroy()
    {
        // This will handle the singleton destruction.
        base.OnDestroy();
        OnGoldChanged = null; // Unsubscribe all listeners to prevent memory leaks
    }
}