using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSceneManager : MonoBehaviour
{
    private List<Save> saves;
    void Start()
    {
        saves = new List<Save>();
        var fileNames = Directory.GetFiles(Application.persistentDataPath + "/saves", "*.sav");

        Debug.Log("saves count: " + fileNames.Length);
        Debug.Log("first save name: " + fileNames[0]);

        for(int i = 0; i < fileNames.Length; i++)
        {
            saves.Add(new Save(Saver.Load(System.Convert.ToString(i + 1))));
        }

        Debug.Log("first save pos: " + saves[0].GetPlayer().PositionX);
    }

    void Update()
    {
        
    }
}
