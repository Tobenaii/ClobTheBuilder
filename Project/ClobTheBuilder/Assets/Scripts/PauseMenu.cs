using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_pauseMenu;
    [SerializeField]
    private GameEvent m_pauseEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            m_pauseMenu.SetActive(true);
            m_pauseEvent.Invoke();
        }
    }
}
