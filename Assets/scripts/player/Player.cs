using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed { get; private set; }
    public float MaxHealth { get; private set; }
    public float Harisma { get; private set; }
    public float Forse { get; private set; }

    private float currentHealth;
    public float CurrentHealth { 
        get 
        {
            return currentHealth;
        }
        
        set 
        {
            if(value >= 0)
            {
                currentHealth -= value;
            }
            else
            {
                throw new System.Exception("damage cannot be less than zero");
            }
        } 
    }

    public float PositionX { 
        get
        {
            return transform.position.x;
        } 
    }

    public float PositionY
    {
        get
        {
            return transform.position.y;
        }
    }

    private Rigidbody2D _rbPlayer;

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(CurrentProgress.currentProgress.GetPlayer().PositionX, CurrentProgress.currentProgress.GetPlayer().PositionY);
    }

    void Update()
    {
        float yAxis = Mathf.Round(Input.GetAxis("Vertical")) * Speed;
        float xAxis = Mathf.Round(Input.GetAxis("Horizontal")) * Speed;
        _rbPlayer.velocity = new Vector2(xAxis, yAxis);
    }

    public void LoadPlayer(float speed, float maxHealth, float currentHealth, float harisma, float forse)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        this.currentHealth = currentHealth;
        Harisma = harisma;
        Forse = forse;
    }

    public void LoadPlayer(Player player)
    {
        Speed = player.Speed;
        MaxHealth = player.MaxHealth;
        currentHealth = player.CurrentHealth;
        Harisma = player.Harisma;
        Forse = player.Forse;
    }

    public void LoadPlayer(PlayerData player)
    {
        Speed = player.Speed;
        MaxHealth = player.MaxHealth;
        currentHealth = player.CurrentHealth;
        Harisma = player.Harisma;
        Forse = player.Forse;
    }
}

public class PlayerData
{
    public PlayerData(float speed, float maxHealth, float currentHealth, Vector2 rbPlayer, float harisma, float forse)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        this.currentHealth = currentHealth;
        _rbPlayer = new Vector2(rbPlayer.x, rbPlayer.y);
        Harisma = harisma;
        Forse = forse;
    }

    public PlayerData() { }

    public PlayerData(PlayerData player)
    {
        Speed = player.Speed;
        MaxHealth = player.MaxHealth;
        this.currentHealth = player.CurrentHealth;
        _rbPlayer = new Vector2(player.PositionX, player.PositionY);
        Harisma = player.Harisma;
        Forse = player.Forse;
    }

    public PlayerData(Player player)
    {
        Speed = player.Speed;
        MaxHealth = player.MaxHealth;
        this.currentHealth = player.CurrentHealth;
        _rbPlayer = new Vector2(player.PositionX, player.PositionY);
        Harisma = player.Harisma;
        Forse = player.Forse;
    }

    public float Speed { get; private set; }
    public float MaxHealth { get; private set; }
    public float Harisma { get; private set; }
    public float Forse { get; private set; }

    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            if (value >= 0)
            {
                currentHealth -= value;
            }
            else
            {
                throw new System.Exception("damage cannot be less than zero");
            }
        }
    }

    public float PositionX
    {
        get
        {
            return _rbPlayer.x;
        }
    }

    public float PositionY
    {
        get
        {
            return _rbPlayer.y;
        }
    }

    private Vector2 _rbPlayer;
}