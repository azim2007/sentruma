using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapInventoryItemController : InventoryItemController
{
    public override void OnClick()
    {
        
    }

    public override InventoryObjectsHolder Holder()
    {
        return CurrentProgress.currentProgress.Inventory;
    }
}
