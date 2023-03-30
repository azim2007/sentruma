using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    private InventoryFactory factory;
    private InventoryController inventory;
    void Start()
    {
        factory = new InventoryFactory();
    }

    void Update()
    {
        
    }

    public void OpenInventory()
    {
        inventory = factory.Instantiate("invUI").GetComponent<InventoryController>();
    }
}
