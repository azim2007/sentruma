using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllotoSceneManager : MonoBehaviour
{
    private Player player;
    void Start()
    {
        var playerFactory = new PlayerFactory();

        player = playerFactory.Instantiate(UnitsIds.pl, new Vector2(0f, 0f)).GetComponent<Player>();
        player.LoadPlayer(CurrentProgress.currentProgress.GetPlayer());
        Debugger.Log(player.MaxHealth + " " + player.SetDamage);

        var dialogFactory = new DialogFactory();
        dialogFactory.Instantiate("dlgMng");
    }

    void Update()
    {
        
    }

    public void WriteProgress()
    {
        CurrentProgress.currentProgress.SetPlayer(new PlayerData(player));
        SceneLoader.LoadScene("saves_menu", "Открываем загрузки");
    }

    public void LoadProgress()
    {
        Saver.Load("0").ToProgress();
        Debugger.Log("position " + CurrentProgress.currentProgress.GetPlayer().PositionX + " " + CurrentProgress.currentProgress.GetPlayer().PositionY);
        SceneManager.LoadScene("alloto_main");
    }
}
