using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFieldController : MonoBehaviour
{
    public InventoryFactory factory;
    void Start()
    {
        factory = new InventoryFactory();
        for(int i = 0; i < 25; i++)
        {
            var o = factory.Instantiate("obj");
            o.AddComponent<InventoryItemInventoryScreenController>();
            o.GetComponent<InventoryItemInventoryScreenController>().SetObject(i);
            o.transform.SetParent(this.transform);
        }
    }
}
