using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private DialogFactory factory;
    private DialogController dialogController;

    private void Start()
    {
        factory = new DialogFactory();
    }

    public void StartDialog(Dialog dialog)
    {
        GetComponent<AllotoSceneManager>().PauseGame();
        dialogController = factory.Instantiate(id: "dlgVw").GetComponent<DialogController>();

        dialogController.ThisDialog = dialog;
    }
}
