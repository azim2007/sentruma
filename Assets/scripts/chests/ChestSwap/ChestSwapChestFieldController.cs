using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapChestFieldController : MonoBehaviour
{
    private InventoryFactory factory;

    public void SetIndexes(int sceneIndex, int chestIndex)
    {
        var buffer = this.GetComponentInParent<ChestSwapController>();
        factory = new InventoryFactory();
        for (int i = 0; i < 10; i++)
        {
            var o = factory.Instantiate("obj");
            o.AddComponent<ChestSwapChestItemController>();
            var controller = o.GetComponent<ChestSwapChestItemController>();
            controller.SetObject(i);
            controller.SetIndexes(sceneIndex, chestIndex);
            controller.sendToBuffer += buffer.SetBuffer;
            o.transform.SetParent(this.transform);
        }
    }
}
