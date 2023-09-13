using UnityEngine;

public class UnitController : ActingObjectsController
{
    public Dialog CurrentDialog { 
        get
        {
            return AllUnitsDialogs.GetById(base.UnitId).GetCurrentOrDefault();
        } 
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void OnRageAction()
    {
        
    }

    public override void OnPeacefulAction()
    {
        var dialogManager = GameObject.FindGameObjectWithTag("GameSceneManager")
            .GetComponent<DialogManager>();
        Debugger.Log($"{UnitName} speak");
        dialogManager.StartDialog(CurrentDialog);
    }
}
