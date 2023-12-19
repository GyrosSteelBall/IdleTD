using UnityEngine;

public interface IUnit
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
}
