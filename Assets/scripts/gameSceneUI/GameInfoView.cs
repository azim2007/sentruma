using UnityEngine;
using System;
using UnityEngine.UI;
public class GameInfoView
{
    private Image state, hp;
    private Text funPhrase, npcName, npcInfo;
    private Button location, map, menu, diary, inventory, funPhraseButton;

    public GameInfoView(Transform transform)
    {
        hp          = transform.GetChild(0).GetComponent<Image>();
        state       = transform.GetChild(1).GetComponent<Image>();

        funPhrase   = transform.GetChild(7).GetComponent<Text>();
        npcName     = transform.GetChild(8).GetComponent<Text>();
        npcInfo     = transform.GetChild(9).GetComponent<Text>();

        location    = transform.GetChild(2).GetComponent<Button>();
        diary       = transform.GetChild(3).GetComponent<Button>();
        map         = transform.GetChild(4).GetComponent<Button>();
        inventory   = transform.GetChild(5).GetComponent<Button>();
        menu        = transform.GetChild(6).GetComponent<Button>();
        funPhraseButton = transform.GetChild(7).GetComponent<Button>();

        SetButtonsOnClick(transform.GetComponent<GameInfoController>());
    }

    private void SetButtonsOnClick(GameInfoController parent)
    {
        funPhraseButton.onClick.AddListener(UpdatePhrase);
        location.onClick.AddListener(parent.NextLocation);
        diary.onClick.AddListener(parent.OpenDiary);
        map.onClick.AddListener(parent.OpenMap);
        inventory.onClick.AddListener(parent.OpenInventory);
        menu.onClick.AddListener(parent.Pause);
    }

    private void UpdatePhrase()
    {
        funPhrase.text = FunPhrases.GetRandom();
    }

    public void UpdateStats(Sprite hp, Sprite state)
    {
        this.hp.sprite = hp;
        this.state.sprite = state;
    }
}

