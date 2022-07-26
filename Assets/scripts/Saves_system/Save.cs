using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
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
    }

    public string Name { get; set; }

    public float PlayerPositionX { get; private set; }
    public float PlayerPositionY { get; private set; }
    public float PlayerMaxHealth { get; private set; }
    public float PlayerCurrentHealth { get; private set; }
    public float PlayerSpeed { get; private set; }

    public Worlds CurrentWorld { get; set; }

    public void SetPlayer(PlayerData player)
    {
        this.PlayerPositionX = player.PositionX;
        this.PlayerPositionX = player.PositionY;
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

