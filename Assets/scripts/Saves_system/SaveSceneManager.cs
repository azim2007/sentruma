using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSceneManager : MonoBehaviour
{
    public GameObject savePanelPrefab;

    private List<GameObject> savePanels;
    private List<Save> saves;
    private Transform content;
    
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        Debug.Log("content name: " + content);

        saves = new List<Save>();
        var fileNames = Directory.GetFiles(Application.persistentDataPath + "/saves", "*.sav");

        Debug.Log("saves count: " + fileNames.Length);
        try { Debug.Log("first save name: " + fileNames[0]); }
        catch { }

        for(int i = 0; i < fileNames.Length; i++)
        {
            saves.Add(new Save(Saver.Load(System.Convert.ToString(i))));
        }

        try { Debug.Log("first save pos: " + saves[0].Name); }
        catch { }

        savePanels = new List<GameObject>();
        foreach(var e in saves)
        {
            savePanels.Add(Instantiate(savePanelPrefab));
            savePanels[savePanels.Count - 1].transform.SetParent(content);
        }
    }

    void Update()
    {
        
    }
}
