using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero/Rare/Chef")]
public class ChefHero : ScriptableObject 
{
    ScriptableObjectHelperClass.Rarity rarity = ScriptableObjectHelperClass.Rarity.rare;
    public float critDamage, critChance;
    public int damage, health;
    public int defense, range;
    public float attackSpeed;
    public GameObject chefObject;
    // Hold relevant stats of hero towers
    // Also hold levels and experience, when a hero levels up his sprite changes to an upgraded version.
    public void Awake()
    {
        chefObject = Instantiate(Resources.Load("HeroPrefabs/Chef/Chef", typeof(GameObject))) as GameObject;
    }



}
