using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Rare", menuName = "data/Rare")]
public class Rare : ScriptableObject, UnitRarityInterface
{

    [SerializeField]
    public List<string> heroArray;
    public int rarityPooling;


    public void Awake()
    {
        
    }

    public string rollUnit()
    {
        int heroListSize = heroArray.Count;
        int randomValue = UnityEngine.Random.Range(0, heroListSize);
        return heroArray[randomValue];
    }


}
