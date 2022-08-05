using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveSceneManager : MonoBehaviour
{
    private GameObject savePanelPrefab;

    private List<GameObject> savePanels;
    private List<SaveItemController> saveControllers;
    private GameObject content;

    private int saveItemsCount = 100;
    
    void Start()
    {
        savePanelPrefab = GameObject.FindGameObjectWithTag("SaveItem");
        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        Debugger.Log("content name: " + content);

        savePanels = new List<GameObject>();
        saveControllers = new List<SaveItemController>();

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

        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.AddComponent<BackButtonManager>();
    }

    void Update()
    {
        
    }

    private void CreateSavePanel(int id, Save value)
    {
        savePanels.Add(Instantiate(savePanelPrefab));
        savePanels[savePanels.Count - 1].transform.SetParent(content.transform);

        saveControllers.Add(savePanels[savePanels.Count - 1].GetComponent<SaveItemController>());
        saveControllers[saveControllers.Count - 1].Id = id;
        saveControllers[saveControllers.Count - 1].Name = value.Name;
        saveControllers[saveControllers.Count - 1].Date = value.Date;
        saveControllers[saveControllers.Count - 1].CurrentWorld = value.CurrentWorld;
    }

    private void CreateSavePanel(int id)
    {
        savePanels.Add(Instantiate(savePanelPrefab));
        savePanels[savePanels.Count - 1].transform.SetParent(content.transform);

        saveControllers.Add(savePanels[savePanels.Count - 1].GetComponent<SaveItemController>());
        saveControllers[saveControllers.Count - 1].Id = id;
        saveControllers[saveControllers.Count - 1].Name = "";
    }
}
