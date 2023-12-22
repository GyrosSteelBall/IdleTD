using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public int baseMaxHealth;
    public int baseAttackDamage;
    public int baseAbilityPower;
    public int baseArmor;
    public int baseMagicResist;
    public float baseAttackSpeed;
    public float baseAttackRange;
    public int baseMaxMana;
    public Sprite enemySprite;
    public GameObject enemyPrefab;
    public float baseMovementSpeed;
}
