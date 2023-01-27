using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoController : MonoBehaviour
{
    private Image state, hp;
    private Text funPhrase, npcName, npcInfo;
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = Vector3.one;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0f, -145f, 1f);
        rectTransform.sizeDelta = new Vector2(0f, -290f);               //ля я хз поч эти значения, но ток так робит
        SetImageLists();
        SetFields();
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

    private void SetFields()
    {
        hp = transform.GetChild(0).GetComponent<Image>();
        state = transform.GetChild(1).GetComponent<Image>();
        funPhrase = transform.GetChild(7).GetComponent<Text>();
        npcName = transform.GetChild(8).GetComponent<Text>();
        npcInfo = transform.GetChild(9).GetComponent<Text>();
    }

    private void UpdateStats()
    {
        state.sprite = states[CurrentProgress.currentProgress.Player.IsRage ? 1 : 0];
        var maxHp = CurrentProgress.currentProgress.Player.MaxHealth;
        var curHp = CurrentProgress.currentProgress.Player.CurrentHealth;
        hp.sprite = hps[(int)Math.Ceiling(curHp * 5 / maxHp) - 1];
        Debugger.Log(((int)Math.Ceiling(curHp * 5 / maxHp) - 1).ToString());
    }

    private void SetImageLists()
    {
        hps = Resources.LoadAll<Sprite>("uiImages/gameSceneUI/hp").Reverse().ToArray();
        states = Resources.LoadAll<Sprite>("uiImages/gameSceneUI/states");
    }

    private Sprite[] hps;
    private Sprite[] states;
}
