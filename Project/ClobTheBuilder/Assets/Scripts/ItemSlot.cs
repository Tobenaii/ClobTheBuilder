using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Inventory m_inventory;
    [SerializeField]
    private int m_slotIndex;
    private int m_itemAmmount;

    private void OnEnable()
    {
        Time.timeScale = 1;
        if (m_slotIndex >= m_inventory.inventoryItems.Count)
            return;
        m_spriteRenderer.sprite = m_inventory.inventoryItems[m_slotIndex].itemSprite;
        m_itemAmmount = m_inventory.inventoryItems[m_slotIndex].itemAmmount;
    }

    public void DestroyItem(GameObject obj)
    {
        m_itemAmmount++;
        Destroy(obj);
    }

    public void SpawnItem()
    {
        if (m_itemAmmount <= 0)
            return;
        m_itemAmmount--;
        //TODO: Object pool this shit
        GameObject item = Instantiate(m_inventory.inventoryItems[m_slotIndex].itemPrefab, null, true);
        item.GetComponent<Item>().m_slot = this;
        item.transform.position = new Vector3(transform.position.x, transform.position.y, 4);
    }

    private void OnMouseDown()
    {
        SpawnItem();
    }
}
