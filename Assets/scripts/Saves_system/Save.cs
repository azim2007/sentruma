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
        Name = CurrentWorld.ToString() + " " + DateTime.UtcNow.ToString("G");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(CurrentWorld.ToString() + "_main");
    }

    public void ToProgress()
    {
        CurrentProgress.currentProgress.Name = Name;
        CurrentProgress.currentProgress.PlayerPositionX = PlayerPositionX;
        CurrentProgress.currentProgress.PlayerPositionY = PlayerPositionY;
        CurrentProgress.currentProgress.PlayerMaxHealth = PlayerMaxHealth;
        CurrentProgress.currentProgress.PlayerCurrentHealth = PlayerCurrentHealth;
        CurrentProgress.currentProgress.PlayerSpeed = PlayerSpeed;
        CurrentProgress.currentProgress.CurrentWorld = CurrentWorld;
        CurrentProgress.currentProgress.Date = Date;
    }
    public Save() { }

    public Save(string name, Player player, Worlds currentWorld)
    {
        Name = name;
        PlayerPositionX = player.PositionX;
        PlayerPositionY = player.PositionY;
        PlayerMaxHealth = player.MaxHealth;
        PlayerCurrentHealth = player.CurrentHealth;
        PlayerSpeed = player.Speed;
        CurrentWorld = currentWorld;
        Date = DateTime.Now;
    }

    public Save(Save save)
    {
        Name = save.Name;
        PlayerPositionX = save.PlayerPositionX;
        PlayerPositionY = save.PlayerPositionY;
        PlayerMaxHealth = save.PlayerMaxHealth;
        PlayerCurrentHealth = save.PlayerCurrentHealth;
        PlayerSpeed = save.PlayerSpeed;
        CurrentWorld = save.CurrentWorld;
        Date = save.Date;
    }

    public string Name { get; set; }

    public float PlayerPositionX { get; private set; }
    public float PlayerPositionY { get; private set; }
    public float PlayerMaxHealth { get; private set; }
    public float PlayerCurrentHealth { get; private set; }
    public float PlayerSpeed { get; private set; }

    public DateTime Date { get; private set; }

    private int currentWorld;

    public Save(string name)
    {
        PlayerPositionX = CurrentProgress.currentProgress.PlayerPositionX;
        PlayerPositionY = CurrentProgress.currentProgress.PlayerPositionY;
        PlayerMaxHealth = CurrentProgress.currentProgress.PlayerMaxHealth;
        PlayerCurrentHealth = CurrentProgress.currentProgress.PlayerCurrentHealth;
        PlayerSpeed = CurrentProgress.currentProgress.PlayerSpeed;
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
        this.PlayerCurrentHealth = player.CurrentHealth;
        this.PlayerSpeed = player.Speed;
    }

    public PlayerData GetPlayer()
    {
        return new PlayerData(
            speed: PlayerSpeed, 
            maxHealth: PlayerMaxHealth, 
            currentHealth: PlayerCurrentHealth, 
            rbPlayer: new Vector2(PlayerPositionX, PlayerPositionY)
        );
    }
}

