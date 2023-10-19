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
    public int whatHeroIsThis;
    public string heroName, descriptorText;
    public int attack, health, defense;
    public float attackSpeed, range, critChance, critDamage;
    public GameObject heroPrefab;
    bool isleftFacing;
    

    public void OnEnable()
    {

    }
    public void Awake()
    {
        /*
        switch (whatHeroIsThis)
        {
            case 0: heroName = "Bowman";rarity = Rarity.common;
                descriptorText = "Basic archer"; attack = 1; health = 1; defense = 1;
                attackSpeed = 1; range = 3; critChance = 1f; critDamage = 195f;
                heroPrefab = Instantiate(Resources.Load("HeroPrefabs/Bowman/Bowman", typeof(GameObject))) as GameObject;

                break;  
            case 1: heroName = "BRuh";
                break;
            default: heroName = "Default";
                break;
        }
        */
    }









}
