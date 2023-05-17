using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoFactory : Factory<string>
{
    public GameInfoFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "gameInfo",
            "pause",
        };

        FillDictionary("prefabs/UI/gameInfo", namesList);
    }
}
