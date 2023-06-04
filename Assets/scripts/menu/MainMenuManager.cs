using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    List<Button> buttons;
    private void Start()
    {
        buttons = new List<Button>();
        var canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        for (int index = 0; index < canvas.childCount; index++)
        {
            buttons.Add(canvas.GetChild(index).gameObject.GetComponent<Button>());
        }

        buttons[0].onClick.AddListener(() => Exit());
        buttons[1].onClick.AddListener(() => StartGame());
        buttons[2].onClick.AddListener(() => Saves());
        buttons[3].onClick.AddListener(() => Settings());
        buttons[4].onClick.AddListener(() => AboutUs());

        InputManager.Manager.Save();
    }

    private void Exit() => Application.Quit();
    private void StartGame()
    {
        CurrentProgress.currentProgress.CurrentWorld = Worlds.alloto;

        CurrentProgress.currentProgress.Player =
           new PlayerData(speed: 2,
                maxHealth: 10,
                currentHealth: 1,
                rbPlayer: new Vector2(0f, 0f),
                harisma: 1,
                forse: 1,
                isRage: false,
                experience: 0,
                level: 0,
                rageLevel: 0,
                armor: null,
                weapon: null
            );
        CurrentProgress.currentProgress.Inventory = new Inventory(inventory: 
            new Tuple<InventoryObject, int>[3]
            {
                Tuple.Create(new DrinkObject() as InventoryObject, 4),
                Tuple.Create(new Money() as InventoryObject, 2),
                Tuple.Create(new DrinkObject() as InventoryObject, 1),
            });
        CurrentProgress.currentProgress.AllGameChests = new AllGameChests("start");
        CurrentProgress.currentProgress.GenerateName();
        CurrentProgress.currentProgress.LoadGame();
    }

    private void Settings()
    {
        Debugger.Log("settings");
        SceneLoader.LoadScene("settings_menu", "Открываем настройки");
    }

    private void AboutUs() => Debugger.Log("AboutUs");
    private void Saves()
    {
        Debugger.Log("Saves");
        SceneLoader.LoadScene("saves_menu", "Открываем загрузки");
    }
}
