using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        CurrentProgress.currentProgress.SetPlayer(this.GetPlayer());
        CurrentProgress.currentProgress.Name = Name;
        CurrentProgress.currentProgress.CurrentWorld = CurrentWorld;
        CurrentProgress.currentProgress.Date = Date;
    }
    public Save() { }

    public Save(string name, Player player, Worlds currentWorld)
    {
        SetPlayer(new PlayerData(player));
        Name = name;
        CurrentWorld = currentWorld;
        Date = DateTime.Now;
    }

    public Save(Save save)
    {
        this.SetPlayer(save.GetPlayer());
        Name = save.Name;
        CurrentWorld = save.CurrentWorld;
        Date = save.Date;
    }

    public string Name { get; set; }

    public float PlayerPositionX { get; private set; }
    public float PlayerPositionY { get; private set; }
    public float PlayerMaxHealth { get; private set; }
    public float PlayerCurrentHealth { get; private set; }
    public float PlayerSpeed { get; private set; }
    public float PlayerHarisma { get; private set; }
    public float PlayerForse { get; private set; }
    public bool PlayerIsRage { get; private set; }

    public DateTime Date { get; private set; }

    private int currentWorld;

    public Save(string name)
    {
        this.SetPlayer(CurrentProgress.currentProgress.GetPlayer());
        CurrentWorld = CurrentProgress.currentProgress.CurrentWorld;
        Date = DateTime.Now;

        GenerateName();
        if(name != "") Name = name;
    }

    public Worlds CurrentWorld { get
        {
            return (Worlds)currentWorld;
        } 
        
        set
        {
            currentWorld = (int)value;
        } 
    }

    public void SetPlayer(PlayerData player)
    {
        this.PlayerPositionX = player.PositionX;
        this.PlayerPositionY = player.PositionY;
        this.PlayerMaxHealth = player.MaxHealth;
        this.PlayerCurrentHealth = player.SetDamage;
        this.PlayerSpeed = player.Speed;
        this.PlayerHarisma = player.Harisma;
        this.PlayerForse = player.Forse;
        this.PlayerIsRage = player.IsRage;
    }

    public PlayerData GetPlayer()
    {
        return new PlayerData(
            speed: PlayerSpeed, 
            maxHealth: PlayerMaxHealth, 
            currentHealth: PlayerCurrentHealth, 
            rbPlayer: new Vector2(PlayerPositionX, PlayerPositionY),
            harisma: PlayerHarisma,
            forse: PlayerForse,
            isRage: PlayerIsRage
        );
    }
}

