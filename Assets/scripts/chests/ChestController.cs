using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : ActingObjectsController
{
    private int sceneIndex, chestIndex;
    private GameObject opened;

    private void Start()
    {
        base.UnitId = UnitsIds.al_jug;    
    }

    public Chest ThisChest { 
        get { return CurrentProgress.currentProgress
                .AllGameChests[sceneIndex][chestIndex].Item1; } }

    public void SetIndexes(int sceneIndex, int chestIndex)
    {
        this.sceneIndex = sceneIndex;
        this.chestIndex = chestIndex;
        opened = transform.GetChild(0).gameObject;
        ThisChest.onOpen += OpenChest;
        if(ThisChest.Opened)
            opened.SetActive(true);
        else
            opened.SetActive(false);
    }

    private void OpenChest()
    {
        opened.SetActive(true);
        var factory = new ChestSwapUIFactory();
        var ui = factory.Instantiate("chstSwpUI");
        ui.GetComponent<ChestSwapController>().SetChestIndexes(sceneIndex, chestIndex);
    }

    private void CloseChest()
    {
        opened.SetActive(false);
    }

    public override void OnRageAction()
    {
        base.OnRageAction();
    }

    public override void OnPeacefulAction()
    {
        ThisChest.Open();
        Debugger.Log("openchest!");
    }
}
