using System;

public class ChestSwapInventoryItemController : InventoryItemController
{
    public event Action<Tuple<InventoryObject, int>, InventoryItemController> sendToBuffer;
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
        return CurrentProgress.currentProgress.Inventory;
    }

    public override bool Equals(object other)
    {
        if (other == null || !(other is ChestSwapInventoryItemController))
            return false;
        ChestSwapInventoryItemController o = (ChestSwapInventoryItemController)other;
        if (o.Index == this.Index && o.ObjName == this.ObjName)
            return true;
        return false;
    }

    public override int GetHashCode()
    {
        return Index ^ ObjName.Length;
    }
}
