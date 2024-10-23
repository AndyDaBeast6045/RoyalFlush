using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory;

    public void addItem(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
            item.onGet();
        } else
        {
            Debug.Log("Item Already in Inventory");
        }
    }

    public void clearInventory() { inventory.Clear(); }
}
