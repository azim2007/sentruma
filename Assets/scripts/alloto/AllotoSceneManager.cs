using UnityEngine;
using UnityEngine.SceneManagement;

public class AllotoSceneManager : MonoBehaviour
{
    private Player player;
    public Player Player { get { return player; } }
    void Start()
    {
        var playerFactory = new PlayerFactory();
        player = playerFactory.Instantiate(UnitsIds.pl, new Vector2(0f, 0f)).GetComponent<Player>();
    }

    void Update()
    {
        
    }

    public void WriteProgress()
    {
        CurrentProgress.currentProgress.Player = player.PlayerData;
        SceneLoader.LoadScene("saves_menu", "Открываем загрузки");
    }
}
