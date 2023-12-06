using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameSceneCamera : MonoBehaviour
{
    private GameObject player;
    private List<Tuple<float, float, float, float>> allotoBordersTopBotLefRig = 
        new List<Tuple<float, float, float, float>>
    {
        Tuple.Create(7f,-4f,-1f,3f),
    };
    public Vector3 CameraPosition()
    {
        var borders = allotoBordersTopBotLefRig;
        if(SceneManager.GetActiveScene().name == "alloto_main")
            borders = allotoBordersTopBotLefRig;

        //не сработает нужно на отдельных сценах все делать
        return new Vector3();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = CameraPosition();
    }
}
