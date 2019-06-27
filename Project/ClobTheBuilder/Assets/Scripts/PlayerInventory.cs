using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory m_inventory;
    [SerializeField]
    private List<GameObject> m_slots;

    private void OnEnable()
    {
        int index = 0;
        foreach (GameObject slot in m_slots)
        {
            if (index >= m_inventory.inventoryItems.Count)
                return;
            Instantiate(m_inventory.inventoryItems[index].item, slot.transform, false);
            index++;
        }
    }
}
