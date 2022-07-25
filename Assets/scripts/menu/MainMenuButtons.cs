using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Exit() => Application.Quit();
    public void StartGame()
    {
        SceneManager.LoadScene("alloto_main");
    }

    public void Settings() => Debug.Log("settings");
    public void AboutUs() => Debug.Log("AboutUs");
    public void Saves() => Debug.Log("Saves");
}
