using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private Image Background { get; set; }

    private Dialog thisDialog;
    public Dialog ThisDialog
    {
        get { return thisDialog; }

        set
        {
            if(thisDialog == null)
            {
                thisDialog = value;
            }

            else
            {
                throw new System.InvalidOperationException("thisDialog isnt epty. You cannot change its value");
            }
        }
    }

    void Start()
    {
        this.GetComponent<Canvas>().worldCamera = 
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Background = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Background.sprite = Resources.Load<Sprite>("backgrounds/" + ThisDialog.BackgroundName);
        Debugger.Log("DialogController Start() background " + (Background != null));
    }

    void Update()
    {
        
    }
}
