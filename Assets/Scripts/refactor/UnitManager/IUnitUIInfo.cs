using UnityEngine;

public interface IUnitUIInfo
{
    string Name { get; }
    int Cost { get; }
    int maxPlacementCount { get; }
    int currentPlacementCount { get; }
    Sprite Icon { get; }
}
