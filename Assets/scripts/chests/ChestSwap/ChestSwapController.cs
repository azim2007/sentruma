using UnityEngine;
using UnityEngine.UI;

public class ChestSwapController : MonoBehaviour
{
    private ChestSwapChestFieldController chestField;
    private Button exit;
    void Start()
    {
        transform.GetComponent<Canvas>().worldCamera = 
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.localScale = Vector3.one;
        GameObject.FindGameObjectWithTag("GameSceneManager").
            GetComponent<AllotoSceneManager>().PauseGame();
        exit = transform.GetChild(1).GetComponent<Button>();
        exit.onClick.AddListener(Close);
    }

    public void SetChestIndexes(int sceneIndex, int chestIndex)
    {
        chestField = transform.GetChild(3).GetComponent<ChestSwapChestFieldController>();
        chestField.SetIndexes(sceneIndex, chestIndex);
    }

    public void Close()
    {
        GameObject.FindGameObjectWithTag("GameSceneManager").
            GetComponent<AllotoSceneManager>().ResumeGame();
        Destroy(gameObject);
    }
}
