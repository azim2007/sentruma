using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private GameObject roof, roofBody;
    void Start()
    {
        roof = transform.GetChild(0).gameObject;
        roofBody = transform.GetChild(1).gameObject;
        roofBody.AddComponent<RoofBodyController>();
    }

    void Update()
    {
        
    }

    public void SetRoofState(bool active)
    {
        roof.SetActive(active);
    }
}
