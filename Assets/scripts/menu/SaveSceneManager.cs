using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSceneManager : MonoBehaviour
{
    public GameObject savePanelPrefab;

    private List<GameObject> savePanels;
    private List<SaveItemController> saveControllers;
    private List<Save> saves;
    private GameObject content;
    
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        Debug.Log("content name: " + content);

        saves = new List<Save>();
        var fileNames = Directory.GetFiles(Application.persistentDataPath + "/saves", "*.sav");

        Debug.Log("saves count: " + fileNames.Length);
        try { Debug.Log("first save name: " + fileNames[0]); }
        catch { }

        foreach(var e in fileNames)
        {
            string name = e.Replace(Application.persistentDataPath + "/saves/save", "");
            name = name.Replace(Application.persistentDataPath + "/saves\\save", "");
            name = name.Replace(".sav", "");
            saves.Add(new Save(Saver.Load(name)));
        }

        try { Debug.Log("first save pos: " + saves[0].Name); }
        catch { }

        savePanels = new List<GameObject>();
        saveControllers = new List<SaveItemController>();
        foreach(var e in saves)
        {
            CreateSavePanel(saveControllers.Count, e);
        }

        for(int i = saveControllers.Count; i < 100; i++)
        {
            CreateSavePanel(i);
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
