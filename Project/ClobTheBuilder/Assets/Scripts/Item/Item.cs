using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum RotationMode { NinetyDegree, Mirror }

    [SerializeField]
    private RotationMode m_rotationMode;
    private List<Renderer> m_renderers;
    private bool m_isGhost;
    private bool m_inRedZone;
    private Rigidbody m_rb;
    [HideInInspector]
    public ItemSlot m_slot;

    public Vector3 SnapToGrid(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
            return new Vector3((int)hit.point.x, (int)hit.point.y, pos.z);
        return pos;
    }

    private void OnEnable()
    {
        Ghostify();
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
    }

    private void Ghostify()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = m_ghostAlpha;
        //m_renderer.material.color = defaultCol;
        m_isGhost = true;
    }

    private void Place()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = 1.0f;
        //m_renderer.material.color = defaultCol;
        m_isGhost = false;
        if (m_inRedZone)
        {
            m_slot.DestroyItem(gameObject);
        }
        OnPlace();
    }

    public virtual void OnEnter(GameObject gameObject)
    {

    }

    public virtual void OnStay(GameObject gameObject)
    {

    }

    public virtual void OnExit(GameObject gameObject)
    {

    }

    public virtual void OnPlace()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        m_inRedZone = true;
        foreach (Renderer rend in m_renderers)
        {
            rend.material.color = new Color(1, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_inRedZone = false;
        foreach (Renderer rend in m_renderers)
        {
            rend.material.color = new Color(1, 1, 1);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!m_isGhost)
            return;
        m_rb.MovePosition(SnapToGrid(transform.position));
        if (Input.GetMouseButtonUp(0))
            Place();
        if (Input.GetButtonDown("RotateLeft"))
        {
            switch (m_rotationMode)
            {
                case RotationMode.NinetyDegree:
                    transform.Rotate(Vector3.forward, 90);
                    break;
                case RotationMode.Mirror:
                    transform.Rotate(Vector3.up, 180);
                    break;
            }
        }
    }
}
