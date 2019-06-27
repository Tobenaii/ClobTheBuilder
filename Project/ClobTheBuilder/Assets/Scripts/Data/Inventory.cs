using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Inventory")]
public class Inventory : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject itemPrefab;
        public Sprite itemSprite;
        public int itemAmmount;
    }

    public List<ItemData> inventoryItems;
}
