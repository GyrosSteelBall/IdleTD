using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Uncommon",menuName = "data/Uncommon")]
public class Uncommon : ScriptableObject, UnitRarityInterface
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
