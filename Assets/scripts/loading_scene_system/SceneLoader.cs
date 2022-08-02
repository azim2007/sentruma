using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string SceneName { get; private set; }
    public static string LoadingText { get; private set; }
    public static void LoadScene(string sceneName, string loadingText)
    {
        SceneName = sceneName;
        LoadingText = loadingText;
        SceneManager.LoadScene("loading_scene");
    }

    public static void LoadScene(string sceneName)
    {
        SceneName = sceneName;
        LoadingText = "Подождите";
        SceneManager.LoadScene("loading_scene");
    }
}
