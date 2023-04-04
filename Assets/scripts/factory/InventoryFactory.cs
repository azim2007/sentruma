using System.Collections.Generic;

public class InventoryFactory : Factory<string>
{
    public InventoryFactory() : base() { }

    public override void SetPrefabs()
    {
        var namesList = new List<string>()
        {
            "obj",
            "invUI",
            "objBar",
        };

        FillDictionary("prefabs/UI/inventoryUI", namesList);
    }
}
