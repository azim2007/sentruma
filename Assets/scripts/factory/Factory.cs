using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory
{
    public Factory()
    {
        SetPrefabs();
    }

    protected Dictionary<UnitsIds, GameObject> prefabs;

    public virtual void SetPrefabs() { }

    public GameObject Instantiate(UnitsIds id, Vector2 position)
    {
        if (!prefabs.ContainsKey(id))
            throw new InvalidOperationException("в фабрике " + this.GetType() + " нет объекта с id " + id);

        return UnityEngine.Object.Instantiate(prefabs[id], position, Quaternion.identity);
    }
}
