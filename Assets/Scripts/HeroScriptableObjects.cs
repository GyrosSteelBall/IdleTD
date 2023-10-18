using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroScriptableObject : ScriptableObject
{
    public enum Rarity
    {
        //needs to be updated
        common, uncommon, rare, legendary, mythic
    }

    Rarity rarity;
    public string heroName, descriptorText;
    public int attack, health, defense;
    public float attackSpeed, range, critChance, critDamage;
    public GameObject heroPrefab;
    bool isleftFacing;
    
    /*
    public AttackComponent attackComponent;
    public SpeedComponent speedComponent;
    public DefenseComponent defenseComponent;
    public HealthComponent healthComponent;
    */








}
