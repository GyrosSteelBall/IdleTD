using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    public string UnitName { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxMana { get; set; }
    public int CurrentMana { get; set; }
    public int AttackDamage { get; set; }
    public int AbilityPower { get; set; }
    public int Armor { get; set; }
    public int MagicResist { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }
    public Sprite UnitSprite { get; set; }
    public GameObject UnitPrefab { get; set; }
    public UnitDataSO UnitData;
    public Unit(UnitDataSO unitData)
    {
        UnitName = unitData.unitName;
        MaxHealth = unitData.baseMaxHealth;
        AttackDamage = unitData.baseAttackDamage;
        AbilityPower = unitData.baseAbilityPower;
        Armor = unitData.baseArmor;
        MagicResist = unitData.baseMagicResist;
        AttackSpeed = unitData.baseAttackSpeed;
        AttackRange = unitData.baseAttackRange;
        UnitSprite = unitData.unitSprite;
        UnitPrefab = unitData.unitPrefab;
        UnitData = unitData;
    }
}