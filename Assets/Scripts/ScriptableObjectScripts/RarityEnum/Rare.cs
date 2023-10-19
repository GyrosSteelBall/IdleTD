using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rare", menuName = "data/Rare")]
public class Rare : ScriptableObject, UnitRarityInterface
{

    [SerializeField]
    public List<string> heroArray;
    public int rarityPooling;


    public void Awake()
    {
        heroArray = new List<string>();
    }

    public void rollUnit()
    {
       
    }
}
