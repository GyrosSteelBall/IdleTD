using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroScriptableObject : ScriptableObject
{
    public string heroName;
    public int attack, health, defense;
    public float attackSpeed, range, critChance, critDamage;
    public GameObject heroPrefab;
    
    /*
    public AttackComponent attackComponent;
    public SpeedComponent speedComponent;
    public DefenseComponent defenseComponent;
    public HealthComponent healthComponent;
    */








}
