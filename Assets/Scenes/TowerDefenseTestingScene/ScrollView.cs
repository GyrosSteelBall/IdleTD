using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    private HeroCollection heroCollection;
    private Transform scrollViewContent;
    private GameObject prefab;
    void Start()
    {
        heroCollection = Resources.Load<HeroCollection>("ScriptableObjects/SummoningScene/HeroCollection");
        foreach (var hero in heroCollection.herocollection)
        {
            string path = "HeroPrefabs/" + hero + "/" + hero;
            GameObject new_hero = Instantiate(Resources.Load(path, typeof(GameObject)),scrollViewContent) as GameObject;
            SpriteRenderer sprite = new_hero.GetComponent<SpriteRenderer>();
            sprite.sortingLayerName = "Layer 3";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
