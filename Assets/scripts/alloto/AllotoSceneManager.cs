using UnityEngine;
using UnityEngine.SceneManagement;

public class AllotoSceneManager : MonoBehaviour
{
    private Player player;
    void Start()
    {
        var playerFactory = new PlayerFactory();

        player = playerFactory.Instantiate(UnitsIds.pl, new Vector2(0f, 0f)).GetComponent<Player>();
        player.PlayerData = CurrentProgress.currentProgress.Player;

        var dialogFactory = new DialogFactory();
        dialogFactory.Instantiate("dlgMng");
    }

    void Update()
    {
        
    }

    public void WriteProgress()
    {
        CurrentProgress.currentProgress.Player = new PlayerData(player);
        SceneLoader.LoadScene("saves_menu", "Открываем загрузки");
    }

    public void LoadProgress()
    {
        Saver.Load("0").ToProgress();
        Debugger.Log("position " + CurrentProgress.currentProgress.Player.PositionX + " " + 
            CurrentProgress.currentProgress.Player.PositionY);
        SceneManager.LoadScene("alloto_main");
    }
}
