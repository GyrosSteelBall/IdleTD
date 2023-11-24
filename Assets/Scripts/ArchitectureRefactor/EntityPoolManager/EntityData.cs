using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Entity System/Entity Data")]
public class EntityData : ScriptableObject
{
    public string entityName;
    public int initialPoolSize = 10;
    // Other relevant stats and properties common to all entities
}
