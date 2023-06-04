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
        var chestFactory = new ChestFactory();
        chestFactory.Instantiate();
    }

    void Update()
    {
        
    }

    public void WriteProgress()
    {
        SceneLoader.LoadScene("saves_menu", "Открываем загрузки");
    }

    //функция не доделана, надо еще реализовать паузу для игровых объектов (чтоб они останавливались)
    public void PauseGame()
    {
        InputManager.Manager.Pause();
    }

    //функция не доделана, надо еще реализовать паузу для игровых объектов (чтоб они возобновляли движ)
    public void ResumeGame()
    {
        InputManager.Manager.Resume();
    }
}
