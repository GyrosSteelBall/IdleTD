using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroDB",menuName ="data/SummoonHeroDB")]
public class SummonerController : ScriptableObject, SummonerControllerInterface
{
    public Common commonObjectScript;
    public Uncommon uncommonObjectScript;
    public Rare rareObjectScript;
    public Legendary legendaryObjectScript;
    public Mythic mythicObjectScript;
    public string rolledHero;
    public string lastRollRarity;

    public void RandomRarity()
    {
        throw new System.NotImplementedException();
    }

    public void roll()
    {
        rollLogic();
        
    }

    public void rollLogic()
    {
        bool isCommon = checkCommon();
        if (isCommon) { rolledHero = commonObjectScript.rollUnit(); return; }
        bool isUnCommon = checkUnCommon();
        if (isUnCommon) { rolledHero = uncommonObjectScript.rollUnit(); return; }
        int RareLegendaryMythicChance = Random.Range(0, 100);
        bool isRare = checkRare(RareLegendaryMythicChance);
        if(isRare) { rolledHero = rareObjectScript.rollUnit(); return; }
        bool isLegendary = checkLegendary(RareLegendaryMythicChance);
        if (isLegendary) { rolledHero = legendaryObjectScript.rollUnit(); return; }
        bool isMythic = checkMythic(RareLegendaryMythicChance);
        if (isMythic) { rolledHero = mythicObjectScript.rollUnit(); return; }


        rolledHero = commonObjectScript.rollUnit();
    }

    public bool checkCommon()
    {
        int commonRollChance = Random.Range(0, 100);
        if( commonRollChance <= 80)
        {
            lastRollRarity = "common";
            return true;
        }
        return false;
    }

    public bool checkUnCommon()
    {
        int UncommonRollChance = Random.Range(0, 100);
        if (UncommonRollChance <= 80)
        {
            lastRollRarity = "Uncommon";
            return true;
        }
        return false;
    }
    public bool checkRare(int randVal)
    {
        if (randVal < 90)
        {
            lastRollRarity = "Rare";
            return true;
        }

        return false;
    }

    public bool checkLegendary(int randVal)
    {
        if (randVal <= 98 && randVal >= 90)
        {
            lastRollRarity = "Legendary";
            return true;
        }
        return false;
    }
    public bool checkMythic(int randVal)
    {
        if (randVal == 99)
        {
            lastRollRarity = "Mythic";
            return true;
        }
        return false;
    }

     public void determineRarity()
    {
        throw new System.NotImplementedException();
    }

}

