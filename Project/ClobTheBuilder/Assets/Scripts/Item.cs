using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Grid m_grid;
    private bool m_isGhost;

    private void OnEnable()
    {
        Ghostify();
    }

    private void Ghostify()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = m_ghostAlpha;
        //m_renderer.material.color = defaultCol;
        m_isGhost = true;
    }

    private void Unghostify()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = 1.0f;
        //m_renderer.material.color = defaultCol;
        m_isGhost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isGhost)
            return;
        Debug.Log(Camera.main.orthographicSize);

        transform.position = m_grid.SnapToGrid(transform.position);
        if (Input.GetMouseButtonDown(0))
            Unghostify();
    }
}
