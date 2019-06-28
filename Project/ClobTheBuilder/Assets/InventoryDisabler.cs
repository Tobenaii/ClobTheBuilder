using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisabler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_slots;

    public void Disable()
    {
        m_slots.SetActive(false);
    }
}
