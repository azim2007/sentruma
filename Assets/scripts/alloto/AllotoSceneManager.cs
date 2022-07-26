using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllotoSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player player;
    void Start()
    {
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.LoadPlayer(CurrentProgress.currentProgress.GetPlayer());
        Debug.Log(player.MaxHealth + " " + player.CurrentHealth);
    }

    void Update()
    {
        
    }
}
