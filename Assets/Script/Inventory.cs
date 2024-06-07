using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += amount;
        }
        else
        {
            items[itemName] = amount;
        }

        UpdateUI();
    }

    public Dictionary<string, int> GetItems()
    {
        return items;
    }

    private void UpdateUI()
    {
        FindObjectOfType<InventoryUI>().UpdateInventoryUI(items);
    }
}
