using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Назад (" + InputManager.Manager.GetKeyName("back") + ")";
        GetComponent<Button>().onClick.AddListener(() => SceneLoader.LoadPreviousScene("Подождите"));
    }

    void Update()
    {
        if (InputManager.Manager.GetKey("back"))
            SceneLoader.LoadPreviousScene("Подождите");
    }
}
