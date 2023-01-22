using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoController : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = Vector3.one;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0f, -145f, 1f);
        rectTransform.sizeDelta = new Vector2(0f, -290f);               //ля я хз поч эти значения, но ток так робит
        SetPlayerData();
    }

    void Update()
    {
        
    }

    private void SetPlayerData()
    {
        Debugger.Log(CurrentProgress.currentProgress.Player.IsRage.ToString());
        CurrentProgress.currentProgress.Player.onChange += () =>
        {
            Debugger.Log("message from stans. IsRage = " + CurrentProgress.currentProgress.Player.IsRage);
        };
    }
}
