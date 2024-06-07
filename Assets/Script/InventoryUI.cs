using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemTemplate;

    public void UpdateInventoryUI(Dictionary<string, int> items)
    {
        // Limpiar la UI anterior
        foreach (Transform child in inventoryPanel.transform)
        {
            if (child.gameObject != itemTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        // Añadir los nuevos elementos
        foreach (KeyValuePair<string, int> item in items)
        {
            GameObject itemGO = Instantiate(itemTemplate, inventoryPanel.transform);
            itemGO.SetActive(true);
            TextMeshProUGUI[] texts = itemGO.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.Key; // Nombre del objeto
            texts[1].text = item.Value.ToString(); // Cantidad del objeto
        }
    }
}
