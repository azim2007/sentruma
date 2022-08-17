using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<TKey>
{
    public Factory()
    {
        SetPrefabs();
    }

    protected Dictionary<TKey, GameObject> prefabs;

    public virtual void SetPrefabs() { }

    protected void FillDictionary(string directoryPath, List<TKey> ids)
    {
        prefabs = new Dictionary<TKey, GameObject>();
        var prefs = Resources.LoadAll<GameObject>(directoryPath);

        if (prefs.Length != ids.Count)
            throw new InvalidOperationException("в папке " + directoryPath + " - " + prefs.Length + " объектов. В листе ids - " + ids.Count + " idшников");

        for(int i = 0; i < ids.Count; i++)
        {
            prefabs.Add(ids[i], prefs[i]);
        }
    }

    public GameObject Instantiate(TKey id, Vector2 position)
    {
        if (!prefabs.ContainsKey(id))
            throw new InvalidOperationException("в фабрике " + this.GetType() + " нет объекта с id " + id);

        return UnityEngine.Object.Instantiate(prefabs[id], position, Quaternion.identity);
    }

    public GameObject Instantiate(TKey id)
    {
        if (!prefabs.ContainsKey(id))
            throw new InvalidOperationException("в фабрике " + this.GetType() + " нет объекта с id " + id);

        return UnityEngine.Object.Instantiate(prefabs[id]);
    }
}
