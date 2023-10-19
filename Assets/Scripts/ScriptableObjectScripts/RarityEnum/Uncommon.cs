using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Uncommon",menuName = "data/Uncommon")]
public class Uncommon : ScriptableObject, UnitRarityInterface
{
    [SerializeField]
    public List<string> heroArray;
    public int rarityPooling;


    public void Awake()
    {
       
    }

    public string rollUnit()
    {
        return heroArray[0];
    }

    public void OnValidate()
    {
        EditorUtility.SetDirty(this);
    }
}
