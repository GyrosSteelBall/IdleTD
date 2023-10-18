using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonHeroButton : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(delegate { changeScene(); });
    }

    public void changeScene()
    {
        Loader.Load(Loader.Scene.SummoningScene);
    }

    
}
