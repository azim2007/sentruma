using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFactory : Factory<string>
{
    public DialogFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "audioSrc",
            "chrctrImg",
            "dlgVw",
            "vrntVw"
        };

        FillDictionary("prefabs/UI/DialogUI", namesList);
    }
}
