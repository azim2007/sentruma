using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFactory : Factory<string>
{
    public MenuFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "cntrlItm",
            "mainMnBtn",
            "svItm",
        };

        FillDictionary("prefabs/UI/MenuUI", namesList);
    }
}
