using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void WriteProgress()
    {
        CurrentProgress.currentProgress.CurrentWorld = Worlds.alloto;
        CurrentProgress.currentProgress.SetPlayer(new PlayerData(player));

        Saver.Save(save: new Save(name: "current"), fileName: "0");
        Debug.Log("date " + CurrentProgress.currentProgress.Name);
    }

    public void LoadProgress()
    {
        Saver.Load("0").ToProgress();
        Debug.Log("position " + CurrentProgress.currentProgress.GetPlayer().PositionX + " " + CurrentProgress.currentProgress.GetPlayer().PositionY);
        SceneManager.LoadScene("alloto_main");
    }
}
