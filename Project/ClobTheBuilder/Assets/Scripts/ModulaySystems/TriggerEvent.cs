using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    private string m_tag = "";
    [SerializeField]
    private GameEvent m_gameEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (m_tag == "")
            m_gameEvent.Invoke();
        else if (other.transform.CompareTag(m_tag))
            m_gameEvent.Invoke();
    }
}
