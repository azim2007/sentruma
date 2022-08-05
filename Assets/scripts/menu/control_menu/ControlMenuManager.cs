using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuManager : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject.AddComponent<BackButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
