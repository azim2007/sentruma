using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
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
            CurrentProgress.currentProgress.Player.IsRage = 
                !CurrentProgress.currentProgress.Player.IsRage;
        }
    }

    IEnumerator Move()
    {
        float yAxis = InputManager.Manager.GetAxis("Vertical") * 
            CurrentProgress.currentProgress.Player.Speed;
        float xAxis = InputManager.Manager.GetAxis("Horizontal") *
            CurrentProgress.currentProgress.Player.Speed;
        rbPlayer.velocity = new Vector2(xAxis, yAxis);
        CurrentProgress.currentProgress.Player.Position 
            = new Vector2(transform.position.x, transform.position.y);
        yield return null;
    }
}

[System.Serializable]
public class PlayerData
{
    public PlayerData(float speed, float maxHealth, float currentHealth, Vector2 rbPlayer, 
        float harisma, float forse, bool isRage)
    {
        this.speed = speed;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.Position = new Vector2(rbPlayer.x, rbPlayer.y);
        this.harisma = harisma;
        this.forse = forse;
        this.isRage = isRage;
    }

    public PlayerData() { }

    public event Action onChange;

    private float speed;
    public float Speed { get { return speed; } set { speed = value; onChange(); } }

    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; onChange(); } }

    private float harisma;
    public float Harisma { get { return harisma; } set { harisma = value; onChange(); } }

    private float forse;
    public float Forse { get { return forse; } set { forse = value; onChange(); } }
    
    private bool isRage;
    public bool IsRage { get { return isRage; } set { isRage = value; onChange(); } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; onChange(); } }

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