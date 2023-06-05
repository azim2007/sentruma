using UnityEngine;
using UnityEngine.UI;
using System;

public class ChestSwapController : MonoBehaviour
{
    private ChestSwapChestFieldController chestField;
    private Button exit;
    private Tuple<InventoryObject, int, InventoryItemController> buffer; 
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

    public void SetBuffer(Tuple<InventoryObject, int> item, InventoryItemController from)
    {
        if(buffer != null && (item == null || 
            (buffer.Item1.Equals(item.Item1) && !from.Equals(buffer.Item3))))
        {
            from.Holder().Add(buffer.Item1, item == null ? buffer.Item2 : buffer.Item2 + item.Item2);
            buffer = null;
            EnableExit();
        }
        else if(buffer != null && from.Equals(buffer.Item3) && buffer.Item1.Equals(item.Item1))
        {
            buffer = Tuple.Create(buffer.Item1, buffer.Item2 + item.Item2, buffer.Item3);
            DisableExit();
        }
        else if(item != null)
        {
            if(buffer != null)
            {
                from.Holder().Add(buffer.Item1, buffer.Item2);
            }

            buffer = Tuple.Create(item.Item1, item.Item2, from);
            DisableExit();
        }
    }

    private void EnableExit()
    {
        exit.enabled = true;
        exit.transform.GetChild(0).GetComponent<Text>().text = "Назад";
    }

    private void DisableExit()
    {
        exit.enabled = false;
        exit.transform.GetChild(0).GetComponent<Text>().text = TextForButton();
    }

    private string TextForButton()
    {
        return "перемещение " + buffer.Item2 + " " +  buffer.Item1.Name;
    }
}
