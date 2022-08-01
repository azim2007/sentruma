using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofBodyController : MonoBehaviour
{
    private BuildingController parent;
    private void Start()
    {
        parent = this.transform.parent.gameObject.GetComponent<BuildingController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            parent.SetRoofState(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            parent.SetRoofState(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            parent.SetRoofState(false);
        }
    }
}
