using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Common", menuName = "data/Common")]
public class Common : ScriptableObject, UnitRarityInterface
{

    [SerializeField] private List<string> heroArray;


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
