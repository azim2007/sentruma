using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    void Start()
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

        canvas.GetChild(1).gameObject.AddComponent<BackButtonManager>();

        canvas.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(
            () => 
                SceneLoader.LoadScene(
                    sceneName: "control_menu", 
                    loadingText: "Открываем настройки управления"
                )
            );
    }

    void Update()
    {
        
    }
}
