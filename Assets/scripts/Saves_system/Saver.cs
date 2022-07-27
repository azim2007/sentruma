using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saver
{
    public static void Save(Save save, string fileName)
    {
        var formatter = new BinaryFormatter();
        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        var path = Application.persistentDataPath + "/saves/save" + fileName + ".sav";
        var stream = new FileStream(path, FileMode.Create);

        Debug.Log("position on save: " + save.GetPlayer().PositionX + " " + save.GetPlayer().PositionY);
        Debug.Log("saved at path: " + path);
        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static Save Load(string fileName)
    {
        var path = Application.persistentDataPath + "/saves/save" + fileName + ".sav";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            Save save = new Save(formatter.Deserialize(stream) as Save);

            stream.Close();
            return save;
        }
        throw new System.Exception("there is no file with name " + path);
    }
}
