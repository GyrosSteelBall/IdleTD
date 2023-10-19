using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonHeroController : MonoBehaviour
{
    SummonerController heroDB;
    // Start is called before the first frame update
    void Start()
    {
        heroDB = Resources.Load<SummonerController>("ScriptableObjects/SummoningScene/SummonerController");
        heroDB.roll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
