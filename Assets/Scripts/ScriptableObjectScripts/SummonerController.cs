using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroDB",menuName ="data/SummoonHeroDB")]
public class SummonerController : ScriptableObject, SummonerControllerInterface
{
    public List<string> heropool;
    public Common commonObjectScript;
    public Uncommon uncommonObjectScript;
    public Rare rareObjectScript;
    public Legendary legendaryObjectScript;
    public Mythic mythicObjectScript;
    public string rolledHero;
    public int arraylength;
    public bool isGenerated = false;

    public void RandomRarity()
    {
        throw new System.NotImplementedException();
    }

    public void roll()
    {

        int total = heropool.Count;
        arraylength = total;
        int randomVal = Random.Range(0, total);
        rolledHero = heropool[randomVal];
    }

    public void Start()
    {
        isGenerated = false;
        heropool = new List<string>();

    }

     public void determineRarity()
    {
        throw new System.NotImplementedException();
    }

}
}
