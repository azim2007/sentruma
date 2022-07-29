﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveItemController : MonoBehaviour
{
    private GameObject save, load; //buttons
    private Text saveName, currentWorld, date, number;
    private GameObject setName; //input field

    public int Id { get; set; }
    public Save Value { get; set; }
    public bool IsEmpty { get { return Value == null; } }
    public bool IsProgressEmpty { get { return CurrentProgress.currentProgress.GetPlayer().MaxHealth == 0; } }

    void Start()
    {
        SetUIObjectsValues();
        CheckSaveable();
        CheckLoadable(); 

        number.text = "" + (Id + 1);
    }

    void Update()
    {
        
    }

    private void Load()
    {
        Value.ToProgress();
        CurrentProgress.currentProgress.LoadGame();
    }

    private void Save()
    {
        string saveName = setName.GetComponent<InputField>().text;
        Value = new Save(saveName);
        Saver.Save(Value, Id.ToString());
        CheckSaveable();
        CheckLoadable();
    }

    private void OnSetNameValueChange(string value)
    {
        load.SetActive(!IsEmpty && value != "");
    }

    private void CheckLoadable()
    {
        load.SetActive(false);
        saveName.text = "тут пусто";
        date.text = "безвременье";
        currentWorld.text = "межпространственное небытие";

        if (!IsEmpty)
        {
            load.SetActive(true);
            saveName.text = Value.Name;
            date.text = Value.Date.ToString("G");
            currentWorld.text = Value.CurrentWorld.ToString();
            load.GetComponent<Button>().onClick.AddListener(() => Load());
        }
    }

    private void CheckSaveable()
    {
        save.GetComponent<Button>().onClick.AddListener(() => Save());
        setName.GetComponent<InputField>().onValueChanged.AddListener(OnSetNameValueChange);
        if (IsProgressEmpty) //проверка: зашел ли пользователь из главного меню при запуске игры (false) или зашел из игровой сцены (true)
        {
            save.SetActive(false);
            setName.SetActive(false);
        }
    }

    private void SetUIObjectsValues()
    {
        save = transform.GetChild(6).gameObject;
        load = transform.GetChild(5).gameObject;

        saveName = transform.GetChild(0).gameObject.GetComponent<Text>();
        currentWorld = transform.GetChild(4).gameObject.GetComponent<Text>();
        date = transform.GetChild(3).gameObject.GetComponent<Text>();
        number = transform.GetChild(8).gameObject.GetComponent<Text>();

        setName = transform.GetChild(7).gameObject;
    }
}
