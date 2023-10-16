using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroScriptableObject : ScriptableObject
{
    public string heroName;
    public int attack, health;
    public float attackSpeed, range, critChance, critDamage;


    GameObject HeroPrefab  = (GameObject) Resources.Load("", typeof(GameObject));
    




}
