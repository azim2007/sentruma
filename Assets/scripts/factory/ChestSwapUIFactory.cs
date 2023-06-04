using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapUIFactory : Factory<string>
{
    public ChestSwapUIFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "chstSwpUI",
        };

        FillDictionary("prefabs/UI/ChestSwapUI", namesList);
    }
}
