using System;

[System.Serializable]
public class Inventory
{
    private Tuple<InventoryObject, int>[] inventory;

    [field: NonSerialized]
    public event Action onChange;
    public Inventory(Tuple<InventoryObject, int>[] inventory)
    {
        onChange += () => { };
        this.inventory = new Tuple<InventoryObject, int>[25];
        for(int i = 0; i < inventory.Length; i++)
        {
            this.inventory[i] = inventory[i];
        }
    }

    public Inventory(Inventory inventory)
    {
        onChange += () => { };
        this.inventory = inventory.inventory;
    }

    public Inventory()
    {
        onChange += () => { };
        this.inventory = new Tuple<InventoryObject, int>[25];
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
        if (index >= 0 && index < 25 && this.inventory[index] != null)
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
        if(inventory[index] == null || !(inventory[index].Item1 is IUsableObject)) return;
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
