using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mythic", menuName = "data/Mythic")]
public class Mythic : ScriptableObject, UnitRarityInterface
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
