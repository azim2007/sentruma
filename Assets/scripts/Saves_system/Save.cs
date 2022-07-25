using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public string Name { get; set; }

    public float PlayerPositionX { get; private set; }
    public float PlayerPositionY { get; private set; }
    public float PlayerMaxHealth { get; private set; }
    public float PlayerCurrentHealth { get; private set; }
    public float PlayerSpeed { get; private set; }

    public Worlds CurrentWorld { get; set; }

    public void SetPlayer(Player player)
    {
        this.PlayerPositionX = player.PositionX;
        this.PlayerPositionX = player.PositionY;
        this.PlayerMaxHealth = player.MaxHealth;
        this.PlayerCurrentHealth = player.CurrentHealth;
        this.PlayerSpeed = player.Speed;
    }

    public Player GetPlayer()
    {
        Player player = new Player(PlayerSpeed, PlayerMaxHealth, PlayerCurrentHealth);
    }
}

