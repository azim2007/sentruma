using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player() { }

    public Player(float speed, float maxHealth, float currentHealth)    {
        Speed = speed;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }


    public float Speed { get; }
    public float MaxHealth { get; }

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
            return _rbPlayer.velocity.x;
        } 
    }

    public float PositionY
    {
        get
        {
            return _rbPlayer.velocity.y;
        }
    }

    public Vector2 Position
    {
        set
        {
            _rbPlayer.position = value;
        }
    }

    private Rigidbody2D _rbPlayer;
    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        _rbPlayer.velocity = new Vector2(xAxis, yAxis);
    }
}
