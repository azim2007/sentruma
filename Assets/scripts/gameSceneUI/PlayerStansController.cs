using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStansController : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = Vector3.one;
        SetPlayerData();
    }

    void Update()
    {
        
    }

    private void SetPlayerData()
    {
        Debugger.Log(CurrentProgress.currentProgress.Player.IsRage.ToString());
        CurrentProgress.currentProgress.Player.onChange += () =>
        {
            Debugger.Log("message from stans. IsRage = " + CurrentProgress.currentProgress.Player.IsRage);
        };
    }
}
