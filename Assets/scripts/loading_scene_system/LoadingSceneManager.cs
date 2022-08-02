using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    private Text loadingText;
    private string loadingSceneName;
    void Start()
    {
        loadingText = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject.GetComponent<Text>();
        loadingText.text = SceneLoader.LoadingText + "...";
        loadingSceneName = SceneLoader.SceneName;
        SceneManager.LoadScene(loadingSceneName);
    }
}
