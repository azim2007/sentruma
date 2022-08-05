using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    [NonSerialized] private static InputManager manager = new InputManager();
    public static InputManager Manager { 
        get 
        { 
            return manager; 
        } 
        
        private set 
        { 
            manager = value; 
        } 
    }

    private InputManager()
    {
        Load();
    }

    private InputManager(Dictionary<string, Tuple<string, KeyCode>> codesNames)
    {
        this.codesNames = codesNames;
    }

    private Dictionary<string, Tuple<string, KeyCode>> codesNames;

    public bool GetKey(string id)
    {
        if (!codesNames.ContainsKey(id)) 
            throw new InvalidOperationException("нет кейкода с айди " + id);

        return Input.GetKey(codesNames[id].Item2);
    }

    public string GetKeyName(string id)
    {
        if (!codesNames.ContainsKey(id))
            throw new InvalidOperationException("нет кейкода с айди " + id);

        return codesNames[id].Item2.ToString();
    }

    public void SetById(string id, KeyCode value)
    {
        if (!codesNames.ContainsKey(id))
            throw new InvalidOperationException("нет кейкода с айди " + id);

        codesNames[id] = new Tuple<string, KeyCode>(codesNames[id].Item1, value);
    }

    public IEnumerable<Tuple<string, Tuple<string, KeyCode>>> GetList()
    {
        foreach(var pair in codesNames)
        {
            yield return new Tuple<string, Tuple<string, KeyCode>>
                (pair.Key, pair.Value);
        }
    }

    public void Save()
    {
        var formatter = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/controls"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/controls");
        }

        var path = Application.persistentDataPath + "/controls/controls.con";
        var stream = new FileStream(path, FileMode.Create);

        Debugger.Log("saved at path: " + path);
        formatter.Serialize(stream, this);
        stream.Close();
    }

    public void Load()
    {
        var path = Application.persistentDataPath + "/controls/controls.con";
        InputManager controls;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            controls = new InputManager(
                (formatter.Deserialize(stream) as InputManager).codesNames);

            stream.Close();
            this.codesNames = 
                controls.codesNames.Count >= DefaultControls().Count ? 
                    controls.codesNames : 
                    DefaultControls();
        }

        else this.codesNames = DefaultControls();

    }

    private Dictionary<string, Tuple<string, KeyCode>> DefaultControls()
    {
        return new Dictionary<string, Tuple<string, KeyCode>>
        {
            { "up", new Tuple<string, KeyCode>( "Идти вверх", KeyCode.W ) },
            { "down", new Tuple<string, KeyCode>( "Идти вниз", KeyCode.S ) },
            { "left", new Tuple<string, KeyCode>( "Идти влево", KeyCode.A ) },
            { "right", new Tuple<string, KeyCode>( "Идти вправо", KeyCode.D ) },
            { "act", new Tuple<string, KeyCode>( "Поговорить (мирный) / ударить (агрессивный)", KeyCode.LeftControl ) },
            { "state", new Tuple<string, KeyCode>( "Сменить состояние (мирный / агрессивный)", KeyCode.Tab ) },
            { "back", new Tuple<string, KeyCode>( "Открыть меню / перейти на предыдущую сцену (из меню)", KeyCode.Escape ) },
        };
    }

    public float GetAxis(string axis)
    {
        var axises = new Dictionary<string, Tuple<string, string>>
        {
            { "Horizontal", new Tuple<string, string>("left", "right") },
            { "Vertical", new Tuple<string, string>("down", "up") }
        };

        if (!axises.ContainsKey(axis))
            throw new InvalidOperationException("нет axis с именем " + axis);

        float result = 0;
        var negativeKeyValue = GetKey(axises[axis].Item1);
        var positiveKeyValue = GetKey(axises[axis].Item2);
        result += negativeKeyValue ? -1 : 0;
        result += positiveKeyValue ? 1 : 0;
        return result;
    }
}