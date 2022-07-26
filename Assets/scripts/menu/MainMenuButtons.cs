using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuButtons : MonoBehaviour
{
    public void Exit() => Application.Quit();
    public void StartGame()
    {
        SceneManager.LoadScene("alloto_main");
        CurrentProgress.currentProgress.CurrentWorld = Worlds.alloto;

        CurrentProgress.currentProgress.SetPlayer(
           new PlayerData(speed: 5, 
                maxHealth: 10, 
                currentHealth: 1, 
                rbPlayer: new Vector2(0f, 0f)
            )
        );
    }

    public void Settings() => Debug.Log("settings");
    public void AboutUs() => Debug.Log("AboutUs");
    public void Saves() => Debug.Log("Saves");
}
