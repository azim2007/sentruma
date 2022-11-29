using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStansController : MonoBehaviour
{
    private Player player;
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = Vector3.one;
    }

    void Update()
    {
        
    }

    public void SetPlayerData(Player player)
    {
        this.player = player;
        Debugger.Log(player.PlayerData.IsRage.ToString());
        player.OnPlayerDataChanged += (playerData) =>
        {
            Debugger.Log("message from stans. IsRage = " + playerData.IsRage);
        };
    }
}
