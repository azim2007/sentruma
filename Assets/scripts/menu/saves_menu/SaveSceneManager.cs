using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveSceneManager : MonoBehaviour
{
    private MenuFactory menuFactory;
    private GameObject content;

    private int saveItemsCount = 100;
    
    void Start()
    {
        menuFactory = new MenuFactory();
        content = GameObject.FindGameObjectWithTag("Canvas").
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        Debugger.Log("content name: " + content);

        for(int i = 0; i < saveItemsCount; i++)
        {
            try
            {
                Save current = Saver.Load(i.ToString());
                CreateSavePanel(i, current);
            }
            catch
            {
                CreateSavePanel(i);
            }

            if(i % 10 == 9) GC.Collect();
        }

        GameObject.FindGameObjectWithTag("Canvas").
            transform.GetChild(2).gameObject.AddComponent<BackButtonManager>();
    }

    void Update()
    {
        
    }

    private SaveItemController CreateSavePanel(int id, Save value)
    {
        var panel = CreateSavePanel(id);
        
        panel.Name = value.Name;
        panel.Date = value.Date;
        panel.CurrentWorld = value.CurrentWorld;
        return panel;
    }

    private SaveItemController CreateSavePanel(int id)
    {
        var panel = menuFactory.Instantiate("svItm").GetComponent<SaveItemController>();
        panel.transform.SetParent(content.transform);
        panel.transform.localScale = new Vector3(1f, 1f, 1f);

        panel.Id = id;
        panel.Name = "";
        return panel;
    }
}
