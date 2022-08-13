using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PlayerFactory : Factory
{
    public PlayerFactory() : base() { }

    public override void SetPrefabs()
    {
        prefabs = new Dictionary<UnitsIds, GameObject>();
        var prefs = Resources.LoadAll<GameObject>("prefabs/player");
        prefabs.Add(UnitsIds.pl, prefs[0]);
    }
}