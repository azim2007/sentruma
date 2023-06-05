using System;
using UnityEngine.UI;

public class ChestSwapChestItemController : InventoryItemController
{
    private int sceneIndex, chestIndex;
    public event Action<Tuple<InventoryObject, int>, InventoryItemController> sendToBuffer;

    public void SetIndexes(int sceneIndex, int chestIndex)
    {
        this.sceneIndex = sceneIndex;
        this.chestIndex = chestIndex;
    }

    public override void OnClick()
    {
        Tuple<InventoryObject, int> send = null;
        if (base.HasObj)
        {
            send = Tuple.Create(Holder().Inventory[base.Index].Item1, 1);
            Holder().Throw(Index, 1);
        }
            
        sendToBuffer(send, this);
    }

    private void OnLongPress()
    {
        Tuple<InventoryObject, int> send = null;
        if (base.HasObj)
        {
            send = Holder().Inventory[base.Index];
            Holder().Throw(Index, Holder().Inventory[base.Index].Item2);
        }

        sendToBuffer(send, this);
    }

    public override InventoryObjectsHolder Holder()
    {
        return CurrentProgress.currentProgress.AllGameChests[sceneIndex][chestIndex].Item1;
    }

    public override bool Equals(object other)
    {
        if (other == null || !(other is ChestSwapChestItemController))
            return false;
        ChestSwapChestItemController o = (ChestSwapChestItemController)other;
        if (o.Index == this.Index && o.ObjName == this.ObjName 
            && sceneIndex == o.sceneIndex && chestIndex == o.chestIndex)
            return true;
        return false;
    }

    public override int GetHashCode()
    {
        return Index * chestIndex ^ ObjName.Length * sceneIndex;
    }
}
