using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapChestItemController : InventoryItemController
{
    private int sceneIndex, chestIndex;
    
    public void SetIndexes(int sceneIndex, int chestIndex)
    {
        this.sceneIndex = sceneIndex;
        this.chestIndex = chestIndex;
    }

    public override void OnClick()
    {
        
    }

    public override InventoryObjectsHolder Holder()
    {
        return CurrentProgress.currentProgress.AllGameChests[sceneIndex][chestIndex].Item1;
    }
}
