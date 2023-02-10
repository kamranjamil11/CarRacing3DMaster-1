using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
        HomeScreen.instance.InitializeAppOpenAd();
        StartCoroutine(HomeScreen.instance.ShowAppOpenAdIfReady());
    }
    IEnumerator LoadScene() 
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }

   
}
