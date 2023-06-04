using System;

[System.Serializable]
public class Save
{
    public void GenerateName()
    {
        Name = CurrentWorld.ToString() + " " + DateTime.Now.ToString("G");
    }

    public void LoadGame()
    {
        SceneLoader.LoadScene(CurrentWorld.ToString() + "_main", "Заходим в мир");
    }

    public void ToProgress()
    {
        CurrentProgress.currentProgress.Player = this.Player;
        CurrentProgress.currentProgress.Name = Name;
        CurrentProgress.currentProgress.CurrentWorld = CurrentWorld;
        CurrentProgress.currentProgress.Date = Date;
        CurrentProgress.currentProgress.Inventory = Inventory;
        CurrentProgress.currentProgress.AllGameChests = AllGameChests;
    }
    public Save() { }

    public Save(string name, Worlds currentWorld)
    {
        Name = name;
        CurrentWorld = currentWorld;
        Date = DateTime.Now;
    }

    public Save(Save save)
    {
        this.Player = save.Player;
        this.Inventory = save.Inventory;
        Name = save.Name;
        CurrentWorld = save.CurrentWorld;
        this.AllGameChests = save.AllGameChests;
        Date = save.Date;
    }

    public string Name { get; set; }

    public PlayerData Player { get; set; }

    public Inventory Inventory { get; set; }

    public AllGameChests AllGameChests { get; set; }

    public DateTime Date { get; private set; }

    private int currentWorld;

    public Save(string name)
    {
        this.Player = CurrentProgress.currentProgress.Player;
        CurrentWorld = CurrentProgress.currentProgress.CurrentWorld;
        this.Inventory = CurrentProgress.currentProgress.Inventory;
        this.AllGameChests = CurrentProgress.currentProgress.AllGameChests;
        Date = DateTime.Now;

        GenerateName();
        if (name != "") Name = name;
    }

    public Worlds CurrentWorld
    {
        get
        {
            return (Worlds)currentWorld;
        }

        set
        {
            currentWorld = (int)value;
        }
    }
}

