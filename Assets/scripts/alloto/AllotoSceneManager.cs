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
        CurrentProgress.currentProgress.SetPlayer(new PlayerData(player));
        StartCoroutine(LoadSaveScene());
    }

    public void LoadProgress()
    {
        Saver.Load("0").ToProgress();
        Debug.Log("position " + CurrentProgress.currentProgress.GetPlayer().PositionX + " " + CurrentProgress.currentProgress.GetPlayer().PositionY);
        SceneManager.LoadScene("alloto_main");
    }

    IEnumerator LoadSaveScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("saves_menu");
        while (!loading.isDone)
        {
            float loadingProgress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log("progress: " + loadingProgress);
            yield return null;
        }
    }
}
