using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    private class LoadingMonoBehavior : MonoBehaviour { }
    //Dummy Class
    public enum Scene{
        TowerDefenseTestingScene,
        SummoningScene,
        Menu,
        Loading
    }

    private static Action onLoaderCallBack;
    private static AsyncOperation loadingAsyncOperation;
    
    public static void Load(Scene scene){
        //Set the Loader callback action to our desired scene  
        onLoaderCallBack = () =>{
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehavior>().StartCoroutine(LoadSceneAsync(scene));
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
        //Load the loading screen
    }

    private static IEnumerator LoadSceneAsync(Scene scene){
        yield return null; // Makes sure we go past 1 frame before loading begins

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while (!loadingAsyncOperation.isDone){
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null){
            return loadingAsyncOperation.progress;
        }
        else {
            return 1f;
        }
    }

    public static void LoaderCallBack(){  
        //Triggered after the first update which will lead to a screen refresh
        //Then we execute the loadercallback function to load desired scene
        if(onLoaderCallBack!=null)
            onLoaderCallBack();
        onLoaderCallBack = null;
    }

}
