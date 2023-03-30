using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator animator;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(CurrentProgress.currentProgress.Player.PositionX,
            CurrentProgress.currentProgress.Player.PositionY);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        StartCoroutine(Move());
        CheckRage();
    }

    private void CheckRage()
    {
        if (InputManager.Manager.GetKeyDown(id: "state"))
        {
            CurrentProgress.currentProgress.Player.IsRage =
                !CurrentProgress.currentProgress.Player.IsRage;
        }
    }

    IEnumerator Move()
    {
        float vert = InputManager.Manager.GetAxis("Vertical");
        float hor = InputManager.Manager.GetAxis("Horizontal");
        float yAxis = vert *
            CurrentProgress.currentProgress.Player.Speed;
        float xAxis = hor *
            CurrentProgress.currentProgress.Player.Speed;
        var isWalk = vert != 0 || hor != 0;
        animator.SetBool("IsWalk", isWalk);
        if(isWalk)
            transform.eulerAngles = GetRotation((int)vert, (int)hor);
        rbPlayer.velocity = new Vector2(xAxis, yAxis);
        CurrentProgress.currentProgress.Player.Position
            = new Vector2(transform.position.x, transform.position.y);
        yield return null;
    }

    Vector3 GetRotation(int vert, int hor)
    {
        var dict = new Dictionary<Tuple<int, int>, float>()
        {
            { Tuple.Create(1, 0), 0 },
            { Tuple.Create(1, 1), -45 },
            { Tuple.Create(0, 1), -90 },
            { Tuple.Create(-1, 1), -135 },
            { Tuple.Create(-1, 0), -180 },
            { Tuple.Create(-1, -1), 135 },
            { Tuple.Create(0, -1), 90 },
            { Tuple.Create(1, -1), 45 },
        };

        return new Vector3(0, 0, dict[Tuple.Create(vert, hor)]);
    } 
}

[System.Serializable]
public class PlayerData
{
    public PlayerData(float speed, float maxHealth, float currentHealth, Vector2 rbPlayer, 
        float harisma, float forse, bool isRage, float experience, float level, float rageLevel)
    {
        this.speed = speed;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.Position = new Vector2(rbPlayer.x, rbPlayer.y);
        this.harisma = harisma;
        this.forse = forse;
        this.isRage = isRage;
        this.experience = experience;
        this.level = level;
        this.rageLevel = rageLevel;
    }

    public PlayerData() { }

    [field: System.NonSerialized]
    public event Action onChange;

    private float rageLevel;
    public float RageLevel { get { return rageLevel; } set { rageLevel = value; onChange(); } }

    private float level;
    public float Level { get { return level; } set { level = value; onChange(); } }

    private float experience;
    public float Experience { get { return experience; } set { experience = value; onChange(); } }

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

    public float MaxExp
    {
        get
        {
            return 50f + (float)Math.Sqrt(level) * 100f; // чтоб не росло слишком быстро
        }
    }

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