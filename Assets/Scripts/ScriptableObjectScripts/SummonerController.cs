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
    public HeroCollection heroCollectionScript;
    public string rolledHero;
    public string lastRollRarity;
    GameObject rolledHeroSpawn;

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
        if (isCommon) { rolledHero = commonObjectScript.rollUnit(); updateHeroCollection(rolledHero); return; }
        bool isUnCommon = checkUnCommon();
        if (isUnCommon) { rolledHero = uncommonObjectScript.rollUnit(); updateHeroCollection(rolledHero); return; }
        int RareLegendaryMythicChance = Random.Range(0, 100);
        bool isRare = checkRare(RareLegendaryMythicChance);
        if(isRare) { rolledHero = rareObjectScript.rollUnit(); updateHeroCollection(rolledHero); return; }
        bool isLegendary = checkLegendary(RareLegendaryMythicChance);
        if (isLegendary) { rolledHero = legendaryObjectScript.rollUnit(); updateHeroCollection(rolledHero); return; }
        bool isMythic = checkMythic(RareLegendaryMythicChance);
        if (isMythic) { rolledHero = mythicObjectScript.rollUnit(); updateHeroCollection(rolledHero); return; }


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

    public void spawnHero()
    {
        string path = "HeroPrefabs/" + rolledHero + "/" + rolledHero;
        rolledHeroSpawn = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        rolledHeroSpawn.transform.position = new Vector3((float)3.89, (float)20.35, (float).4);
        SpriteRenderer sprite = rolledHeroSpawn.GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "Layer 3";
    }

     public void determineRarity()
    {
        throw new System.NotImplementedException();
    }

    public void updateHeroCollection(string hero)
    {
        heroCollectionScript.herocollection.Add(hero);
    }

}

