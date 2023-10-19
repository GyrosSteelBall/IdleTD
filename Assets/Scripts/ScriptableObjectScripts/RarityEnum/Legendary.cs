using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Legendary", menuName = "data/Legendary")]
public class Legendary : ScriptableObject,UnitRarityInterface
{

    [SerializeField]
    public List<string> heroArray;
    public int rarityPooling;


    public void Awake()
    {
        heroArray = new List<string>();
    }

    public string rollUnit()
    {
        return heroArray[0];    
    }
}
