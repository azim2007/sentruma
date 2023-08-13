using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllotoGameObjectsFactory : Factory<string>
{
    public AllotoGameObjectsFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "man",
            "door",
            "house",
            "jug",
        };

        FillDictionary("prefabs/alloto", namesList);
    }
}
