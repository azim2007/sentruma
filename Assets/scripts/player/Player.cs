using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; }}

    public event Action<PlayerData> OnPlayerDataChanged;

    private Rigidbody2D rbPlayer;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerData = CurrentProgress.currentProgress.Player;
        transform.position = new Vector2(CurrentProgress.currentProgress.Player.PositionX, 
            CurrentProgress.currentProgress.Player.PositionY);
    }

    void Update()
    {
        StartCoroutine(Move());
        CheckRage();
    }

    private void CheckRage()
    {
        if(InputManager.Manager.GetKeyDown(id: "state"))
        {
            PlayerData.IsRage = !PlayerData.IsRage;
            OnPlayerDataChanged(playerData);
        }
    }

    IEnumerator Move()
    {
        float yAxis = InputManager.Manager.GetAxis("Vertical") * PlayerData.Speed;
        float xAxis = InputManager.Manager.GetAxis("Horizontal") * PlayerData.Speed;
        rbPlayer.velocity = new Vector2(xAxis, yAxis);
        PlayerData.Position = new Vector2(transform.position.x, transform.position.y);
        yield return null;
    }
}

[System.Serializable]
public class PlayerData
{
    public PlayerData(float speed, float maxHealth, float currentHealth, Vector2 rbPlayer, 
        float harisma, float forse, bool isRage)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        this.currentHealth = currentHealth;
        Position = new Vector2(rbPlayer.x, rbPlayer.y);
        Harisma = harisma;
        Forse = forse;
        IsRage = isRage;
    }

    public PlayerData() { }

    public PlayerData(PlayerData player) : this(player.Speed, player.MaxHealth, player.CurrentHealth,
        player.Position, player.Harisma, player.Forse, player.IsRage)
    { }

    public PlayerData(Player player) : this(player.PlayerData)
    { }

    public float Speed { get; set; }
    public float MaxHealth { get; set; }
    public float Harisma { get; set; }
    public float Forse { get; set; }
    public bool IsRage { get; set; }

    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }
    public void SetDamage(float damageValue)
    {
        if (damageValue >= 0)
        {
            currentHealth -= damageValue;
        }
        else
        {
            throw new System.Exception("damage cannot be less than zero");
        }
    }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public Vector2 Position { 
        get
        {
            return new Vector2(PositionX, PositionY);
        }
        set
        {
            PositionX = value.x;
            PositionY = value.y;
        }
    }
}