using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Common", menuName = "data/Common")]
public class Common : ScriptableObject, UnitRarityInterface
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
        int heroListSize = heroArray.Count;
        int randomValue = UnityEngine.Random.Range(0, heroListSize);
        return heroArray[randomValue];
    }
  



}
