using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapInventoryFieldController : MonoBehaviour
{
    private InventoryFactory factory;
    void Start()
    {
        factory = new InventoryFactory();
        for (int i = 0; i < 25; i++)
        {
            var o = factory.Instantiate("obj");
            o.AddComponent<ChestSwapInventoryItemController>();
            o.GetComponent<ChestSwapInventoryItemController>().SetObject(i);
            o.transform.SetParent(this.transform);
        }
    }
}
