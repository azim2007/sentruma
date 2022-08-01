using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSceneManager : MonoBehaviour
{
    private GameObject savePanelPrefab;

    private List<GameObject> savePanels;
    private List<SaveItemController> saveControllers;
    private GameObject content;

    private int saveItemsCount = 20;
    
    void Start()
    {
        savePanelPrefab = GameObject.FindGameObjectWithTag("SaveItem");
        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        Debug.Log("content name: " + content);

        savePanels = new List<GameObject>();
        saveControllers = new List<SaveItemController>();

        for(int i = 0; i < saveItemsCount; i++)
        {
            try
            {
                CreateSavePanel(i, Saver.Load(i.ToString()));
            }
            catch
            {
                CreateSavePanel(i);
            }
        }
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
        saveControllers[saveControllers.Count - 1].Value = value;
    }

    private void CreateSavePanel(int id)
    {
        savePanels.Add(Instantiate(savePanelPrefab));
        savePanels[savePanels.Count - 1].transform.SetParent(content.transform);

        saveControllers.Add(savePanels[savePanels.Count - 1].GetComponent<SaveItemController>());
        saveControllers[saveControllers.Count - 1].Id = id;
    }
}
