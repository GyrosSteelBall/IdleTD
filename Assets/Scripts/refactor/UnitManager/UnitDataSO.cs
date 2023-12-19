using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData", order = 1)]
public class UnitDataSO : ScriptableObject
{
    public string unitName;
    public int baseMaxHealth;
    public int baseAttackDamage;
    public int baseAbilityPower;
    public int baseArmor;
    public int baseMagicResist;
    public float baseAttackSpeed;
    public float baseAttackRange;
    public int baseMaxMana;
    public Sprite unitSprite;
    public GameObject unitPrefab;
}
