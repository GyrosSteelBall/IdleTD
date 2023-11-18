using UnityEngine;

public abstract class UpgradeEffect : ScriptableObject
{
    public string effectName;
    public Sprite icon;
    public int cost;
    public string description;

    public abstract void ApplyEffect(Unit unit);
}