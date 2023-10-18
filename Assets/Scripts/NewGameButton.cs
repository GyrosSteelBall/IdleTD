using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public Button button;
    public void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(delegate { changeScene(); });
        
    }
    public void changeScene()
    {
        Loader.Load(Loader.Scene.TowerDefenseTestingScene);
        Loader.LoaderCallBack();
    }
}
