using UnityEngine;

[System.Serializable]
public abstract class InventoryObject
{
    const string path = "uiImages/inventoryUI/inventoryObjects/";
    static public Sprite noneImage = Resources.Load<Sprite>(path + "none");
    private string imagePath;
    private string name;
    private string description;
    public InventoryObject(string imagePath, string name, string description)
    {
        this.imagePath = imagePath;
        this.name = name;
        this.description = description;
    }

    public string Description
    {
        get { return description; }
    }
    public string Name
    {
        get { return name; }
    }

    public string Path
    {
        get { return path + imagePath; }
    }

    public bool IsUsable
    {
        get
        {
            return this is IUsableObject;
        }
    }

    public void Use()
    {
        if (this.IsUsable)
        {
            var o = this as IUsableObject;
            o.Effect();
        }
    }

    public Sprite GetImage()
    {
        return Resources.Load<Sprite>(Path);
    }

    public override bool Equals(System.Object other)
    {
        InventoryObject obj = other as InventoryObject;
        if (obj == null) 
        {
            Debugger.Log("obj is null");
            return false; 
        }

        return obj.imagePath == this.imagePath && obj.name == this.name;
    }

    public override int GetHashCode()
    {
        return imagePath.GetHashCode() * 3 ^ name.GetHashCode();
    }
}

[System.Serializable]
public abstract class Armor : InventoryObject, IUsableObject
{
    const string armorPath = "uiImages/inventoryUI/armor/";
    private float damageLossPercent;
    private string gameImageFolderPath;
    public float DamageLossPercent { get { return damageLossPercent; } }
    public Armor(string imagePath, string name, string description, 
        float damageLossPercent, string folderPath) :
        base(imagePath, name, description)
    {
        this.damageLossPercent = damageLossPercent;
        this.gameImageFolderPath = folderPath + "/";
    }

    public string FolderPath
    {
        get { return armorPath + gameImageFolderPath; }
    }

    public Sprite InventoryImage
    {
        get { return Resources.Load<Sprite>(FolderPath + "armorInventory"); }
    }

    void IUsableObject.Effect()
    {
        for (int i = 0; i < 25; i++)
        {
            if (CurrentProgress.currentProgress.Inventory[i].Item1.Name == this.Name)
            {
                CurrentProgress.currentProgress.Inventory.Throw(i, 1);
            }
        }

        if(CurrentProgress.currentProgress.Player.Armor != null)
            CurrentProgress.currentProgress.Inventory
                .Add(CurrentProgress.currentProgress.Player.Armor, 1);
        CurrentProgress.currentProgress.Player.Armor = this;
    }
}

[System.Serializable]
public abstract class Weapon : InventoryObject, IUsableObject
{
    const string weaponPath = "uiImages/inventoryUI/weapon/";
    private float damage;
    private string gameImageFolderPath;
    public float Damage { get { return damage; } }
    public Weapon(string imagePath, string name, string description, float damage, string folderPath) :
        base(imagePath, name, description)
    {
        this.damage = damage;
        this.gameImageFolderPath = folderPath + "/";
    }

    public string FolderPath
    {
        get { return weaponPath + gameImageFolderPath; }
    }

    public Sprite InventoryImage
    {
        get { return Resources.Load<Sprite>(FolderPath + "weaponInventory"); }
    }

    void IUsableObject.Effect()
    {
        for (int i = 0; i < 25; i++)
        {
            var item = CurrentProgress.currentProgress.Inventory[i];
            if (item != null && item.Item1.Name == this.Name)
            {
                //CurrentProgress.currentProgress.Inventory.Throw(i, 1);
                break;
            }
        }

        if(CurrentProgress.currentProgress.Player.Weapon != null)
            CurrentProgress.currentProgress.Inventory
                .Add(CurrentProgress.currentProgress.Player.Weapon, 1);
        CurrentProgress.currentProgress.Player.Weapon = this;
    }
}

public interface IUsableObject
{
    void Effect();
}

[System.Serializable]
public class DrinkObject : InventoryObject, IUsableObject
{
    public DrinkObject() : base("drink", "пойло", 
        "дает вам 5 опыта. осторожно, употребление алкоголя вредит вашему здоровью!!") { }
    void IUsableObject.Effect()
    {
        CurrentProgress.currentProgress.Player.Experience += 5;
    }
}

[System.Serializable]
public class Money : InventoryObject
{
    public Money() : base("money", "деньги", "на это можно что-нибудь купить у жителей") { }
}

[System.Serializable]
public class AllotoWeapon : Weapon
{
    public AllotoWeapon() : base("allotoWeapon", "оружие аллото", 
        "сие орудие дарованно аллото просвященным", 3, "alloto")
    { }
}

[System.Serializable]
public class RoitelWeapon : Weapon
{
    public RoitelWeapon() : base("roitelWeapon", "оружие роитель",
        "сие орудие дарованно роителем", 5, "roitel")
    { }
}