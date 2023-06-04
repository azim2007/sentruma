using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwapChestFieldController : MonoBehaviour
{
    private InventoryFactory factory;

    public void SetIndexes(int sceneIndex, int chestIndex)
    {
        factory = new InventoryFactory();
        for (int i = 0; i < 10; i++)
        {
            var o = factory.Instantiate("obj");
            o.AddComponent<ChestSwapChestItemController>();
            o.GetComponent<ChestSwapChestItemController>().SetObject(i);
            o.GetComponent<ChestSwapChestItemController>().SetIndexes(sceneIndex, chestIndex);
            o.transform.SetParent(this.transform);
        }
    }
}
