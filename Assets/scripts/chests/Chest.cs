using System;
using System.Linq;

[Serializable]
public class Chest : InventoryObjectsHolder
{
    private InventoryObject key;
    public InventoryObject Key { get { return key; } }

    private string type;
    public string Type { get { return type; } }

    private bool opened;

    [field: NonSerialized]
    public event Action onOpen;
    public bool Opened { get { return opened; } }
    public Chest (InventoryObject key, string type) : this(new Tuple<InventoryObject, int>[1], key, type) { }
    public Chest(Tuple<InventoryObject, int>[] chest, InventoryObject key, string type) : base(10, chest) 
    {
        this.key = key;
        this.type = type;
        opened = false;
        onOpen = () => { };
    }
    public Chest(Chest chest) : this(chest.Inventory, chest.key, chest.type) { }
    
    public bool CanOpen
    {
        get
        {
            return key == null || CurrentProgress.currentProgress.Inventory.Inventory
                .Any((item) => item.Item1.Equals(key));
        }
    }

    public bool Open()
    {
        opened = CanOpen;
        if(CanOpen) onOpen();
        return CanOpen;
    }
}
