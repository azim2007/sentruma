using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuManager : MonoBehaviour
{
    private GameObject content;
    private GameObject itemPrefab;
    private BackButtonManager backButtonManager;
    void Start()
    {
        backButtonManager = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject.AddComponent<BackButtonManager>();

        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        itemPrefab = GameObject.FindGameObjectWithTag("SaveItem");

        CreateControlsItemsList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateControlsItemsList()
    {
        foreach(var key in InputManager.Manager.GetList())
        {
            CreateControlItem(key);
        }
    }

    private void CreateControlItem(Tuple<string, Tuple<string, KeyCode>> value)
    {
        var item = Instantiate(itemPrefab).AddComponent<ControlItemController>();
        item.transform.SetParent(content.transform);
        item.SceneManager = this;
        item.Value = value;
    }

    public void SetKey(ControlItemController sender)
    {
        string id = sender.Id;
        backButtonManager.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(ListenKey());

        IEnumerator ListenKey()
        {
            bool hasPressed = false;
            var stopList = new List<KeyCode>() 
            { 
                KeyCode.None,
                KeyCode.Mouse0,
                KeyCode.Mouse1,
                KeyCode.Mouse2,
                KeyCode.Mouse3,
                KeyCode.Mouse4,
                KeyCode.Mouse5,
                KeyCode.Mouse6
            };

            sender.SetLoadingText();

            while (!hasPressed)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))){
                    if (!stopList.Contains(key) && Input.GetKey(key))
                    {
                        hasPressed = true;
                        InputManager.Manager.SetById(id, key);
                        Debugger.Log("key setted for " + id);
                        break;
                    }
                }

                yield return null;
            }

            InputManager.Manager.Save();
            sender.UpdateKeyName();
            yield return new WaitForSeconds(0.2f);
            backButtonManager.gameObject.SetActive(true);
            yield return null;
        }
    }
}
