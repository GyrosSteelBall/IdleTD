using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroDB",menuName ="data/SummoonHeroDB")]
public class SummoningHeroDB : ScriptableObject
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

    public void formDB()
    {
        if (isGenerated == false)
        {
            for (int i = 0; i < commonObjectScript.heroArray.Count; i++)
            {
                heropool.Add(commonObjectScript.heroArray[i]);
            }

            for (int j = 0; j < uncommonObjectScript.heroArray.Count; j++)
            {
                heropool.Add(uncommonObjectScript.heroArray[j]);
            }


            for (int k = 0; k < rareObjectScript.heroArray.Count; k++)
            {
                heropool.Add(rareObjectScript.heroArray[k]);
            }

            for (int l = 0; l < legendaryObjectScript.heroArray.Count; l++)
            {
                heropool.Add(legendaryObjectScript.heroArray[l]);
            }
            for (int m = 0; m < mythicObjectScript.heroArray.Count; m++)
            {
                heropool.Add(mythicObjectScript.heroArray[m]);
            }
            isGenerated = true;
        }
        
       
      


    }
 
}
