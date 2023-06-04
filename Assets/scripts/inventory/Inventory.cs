using System;

[System.Serializable]
public abstract class InventoryObjectsHolder
{
    private Tuple<InventoryObject, int>[] inventory;
    public Tuple<InventoryObject, int>[] Inventory { get { return inventory; } }

    [field: NonSerialized]
    public event Action onChange;

    public InventoryObjectsHolder(int count)
    {
        onChange = () => { };
        inventory = new Tuple<InventoryObject, int>[count];
    }

    public InventoryObjectsHolder(int count, Tuple<InventoryObject, int>[] inventory) : this(count)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            this.inventory[i] = inventory[i];
        }
    }

    public void ResetOnChange()
    {
        onChange = () => { };
    }

    public bool Add(InventoryObject obj, int count)
    {
        for (int i = 0; i < this.inventory.Length; i++)
        {
            if (this.inventory[i] != null && this.inventory[i].Item1.Path == obj.Path)
            {
                this.inventory[i] =
                    Tuple.Create(this.inventory[i].Item1, this.inventory[i].Item2 + count);
                onChange();
                return true;
            }
        }

        for (int i = 0; i < this.inventory.Length; i++)
        {
            if (this.inventory[i] == null)
            {
                this.inventory[i] = Tuple.Create(obj, count);
                onChange();
                return true;
            }
        }

        onChange();
        Debugger.Log("inventory is full");
        return false;
    }

    public void Throw(int index, int count)
    {
        if (index >= 0 && index < inventory.Length && this.inventory[index] != null)
        {
            var newCount = this.inventory[index].Item2 - count;
            inventory[index] = (newCount > 0) ?
                Tuple.Create(this.inventory[index].Item1, newCount) :
                null;
        }

        onChange();
    }

    public void Use(int index)
    {
        if (inventory[index] == null || !(inventory[index].Item1 is IUsableObject)) return;
        var o = inventory[index].Item1 as IUsableObject;
        o.Effect();
        Throw(index, 1);
    }

    public Tuple<InventoryObject, int> this[int index]
    {
        get
        {
            return this.inventory[index];
        }
    }
}

[System.Serializable]
public class Inventory : InventoryObjectsHolder
{
    public Inventory(Tuple<InventoryObject, int>[] inventory) : base(25, inventory) { }

    public Inventory(Inventory inventory) : this(inventory.Inventory) { }

    public Inventory() : base(25) { }
}
