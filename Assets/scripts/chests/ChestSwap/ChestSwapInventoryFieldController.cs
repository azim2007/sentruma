using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapInventoryFieldController : MonoBehaviour
{
    private InventoryFactory factory;
    void Start()
    {
        var buffer = this.GetComponentInParent<ChestSwapController>();
        factory = new InventoryFactory();
        for (int i = 0; i < 25; i++)
        {
            var o = factory.Instantiate("obj");
            o.AddComponent<ChestSwapInventoryItemController>();
            var controller = o.GetComponent<ChestSwapInventoryItemController>();
            controller.SetObject(i);
            o.transform.SetParent(this.transform);
            controller.sendToBuffer += buffer.SetBuffer;
        }
    }
}
