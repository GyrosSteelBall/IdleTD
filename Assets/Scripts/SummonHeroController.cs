using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonHeroController : MonoBehaviour
{
    SummoningHeroDB heroDB;
    // Start is called before the first frame update
    void Start()
    {
        heroDB = Resources.Load<SummoningHeroDB>("ScriptableObjects/SummoningScene/HeroDB");
        heroDB.formDB();
        heroDB.roll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
