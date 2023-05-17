using UnityEngine;
using UnityEngine.UI;

public class PauseUIController : MonoBehaviour
{
    GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameSceneManager");
        manager.GetComponent<AllotoSceneManager>().PauseGame();
        this.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        this.transform.localScale = Vector3.one;
        ButtonsSet();
    }

    private void ButtonsSet()
    {
        this.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Resume);
        this.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Saves);
        this.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Settings);
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(Menu);
        this.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(Application.Quit);
    }

    private void Resume()
    {
        manager.GetComponent<AllotoSceneManager>().ResumeGame();
        Destroy(this.gameObject);
    }

    private void Saves()
    {
        manager.GetComponent<AllotoSceneManager>().ResumeGame();
        SceneLoader.LoadScene("saves_menu", "Подождите");
    }

    private void Settings()
    {
        manager.GetComponent<AllotoSceneManager>().ResumeGame();
        SceneLoader.LoadScene("settings_menu", "Подождите");
    }

    private void Menu()
    {
        manager.GetComponent<AllotoSceneManager>().ResumeGame();
        SceneLoader.LoadScene("main_menu", "Подождите");
    }

    void Update()
    {
        
    }
}
