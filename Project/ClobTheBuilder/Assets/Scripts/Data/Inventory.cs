using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Inventory")]
public class Inventory : ScriptableObject
{
    [System.Serializable]
    public class InventoryData
    {
        public ItemData item;
        public int itemAmmount;
    }

    public List<InventoryData> inventoryItems;
}
