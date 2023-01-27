using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoController : MonoBehaviour
{
    private GameInfoView view;
    private Sprite[] hps;
    private Sprite[] states;
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = Vector3.one;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0f, -145f, 1f);
        rectTransform.sizeDelta = new Vector2(0f, -290f);               //ля я хз поч эти значения, но ток так робит
        SetImageLists();
        view = new GameInfoView(transform);
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
            UpdateStats();
        };

        UpdateStats();
    }

    private void UpdateStats()
    {
        var st = states[CurrentProgress.currentProgress.Player.IsRage ? 1 : 0];
        var maxHp = CurrentProgress.currentProgress.Player.MaxHealth;
        var curHp = CurrentProgress.currentProgress.Player.CurrentHealth;
        var hp = hps[(int)Math.Ceiling(curHp * 5 / maxHp) - 1];
        view.UpdateStats(hp, st);
    }

    private void SetImageLists()
    {
        hps = Resources.LoadAll<Sprite>("uiImages/gameSceneUI/hp").Reverse().ToArray();
        states = Resources.LoadAll<Sprite>("uiImages/gameSceneUI/states");
    }
}
