using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Legendary", menuName = "data/Legendary")]
public class Legendary : ScriptableObject,UnitRarityInterface
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
