using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string SceneName { get; private set; }
    private static string PreviousSceneName { 
        get 
        {
            return previousScenesNames[previousScenesNames.Count - 1];
        }

        set 
        { 
            for(int i = previousScenesNames.Count - 1; i >= 0; i--)
            {
                if (previousScenesNames[i] == value && PreviousSceneName.Contains("_menu"))
                {
                    previousScenesNames.RemoveRange(i, previousScenesNames.Count - i);
                    break;
                }
            }

            previousScenesNames.Add(value);
        } 
    }
    private static List<string> previousScenesNames = new List<string>();
    public static string LoadingText { get; private set; }
    public static void LoadScene(string sceneName, string loadingText)
    {
        SceneName = sceneName;
        LoadingText = loadingText;
        PreviousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("loading_scene");
    }

    public static void LoadPreviousScene(string loadingText)
    {
        PreviousSceneName = SceneManager.GetActiveScene().name;
        previousScenesNames.RemoveAt(previousScenesNames.Count - 1);
        LoadScene(PreviousSceneName, loadingText);
    }
}
