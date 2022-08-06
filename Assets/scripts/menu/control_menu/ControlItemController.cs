using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlItemController : MonoBehaviour
{
    private Tuple<string, Tuple<string, KeyCode>> value;
    public Tuple<string, Tuple<string, KeyCode>> Value { 
        private get { return value; } 
        set { this.value = 
                new Tuple<string, Tuple<string, KeyCode>>
                    ( value.Item1, 
                    new Tuple<string, KeyCode>
                        (value.Item2.Item1, 
                        value.Item2.Item2)
                    );
        } 
    }

    private string Action { get { return Value.Item2.Item1; } }
    private KeyCode KeyCode { get { return Value.Item2.Item2; } }
    public string Id { get { return Value.Item1; } }

    private Text action, keyName;
    private Button setKey;

    public ControlMenuManager SceneManager { private get; set; }

    void Start()
    {
        action = transform.GetChild(0).GetComponent<Text>();
        setKey = transform.GetChild(1).GetComponent<Button>();
        keyName = setKey.transform.GetChild(0).GetComponent<Text>();

        action.text = Action;
        keyName.text = KeyCode.ToString();

        setKey.onClick.AddListener(() => SceneManager.SetKey(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLoadingText()
    {
        keyName.text = "<color=#EB4747FF>нажмите на клавишу...</color>";
    }

    public void UpdateKeyName()
    {
        Value = InputManager.Manager.GetById(Id);
        keyName.text = InputManager.Manager.GetKeyName(Id);
    }
}
