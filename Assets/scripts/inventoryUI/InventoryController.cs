using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    InventoryView view;
    private void Start()
    {
        InputManager.Manager.Pause();
        this.GetComponent<Canvas>().worldCamera = 
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        view = new InventoryView(this.transform);
        SetData();
    }

    private void SetData()
    {
        CurrentProgress.currentProgress.Player.onChange += this.UpdateStats;
        UpdateStats();
    }

    private void UpdateStats()
    {
        view.UpdateStats(
            force:      CurrentProgress.currentProgress.Player.Forse, 
            level:      CurrentProgress.currentProgress.Player.Level, 
            harisma:    CurrentProgress.currentProgress.Player.Harisma, 
            experience: CurrentProgress.currentProgress.Player.Experience,
            rageLevel:  CurrentProgress.currentProgress.Player.RageLevel);
    }

    public void CloseInventory()
    {
        CurrentProgress.currentProgress.Player.onChange -= this.UpdateStats;
        InputManager.Manager.Resume();
        Destroy(this.gameObject);
        Debugger.Log("close inv");
    }
}
