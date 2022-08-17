using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PlayerFactory : Factory<UnitsIds>
{
    public PlayerFactory() : base() { }

    public override void SetPrefabs()
    {
        FillDictionary(directoryPath: "prefabs/player", ids: new List<UnitsIds> { UnitsIds.pl });
    }
}