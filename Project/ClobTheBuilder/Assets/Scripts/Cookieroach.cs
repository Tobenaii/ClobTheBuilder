using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookieroach : Item
{
    [SerializeField]
    private float m_moveDistance;
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_stopTime;
    [SerializeField]
    private float m_rotateSpeed;

    private bool m_startMoving;
    private Vector3 m_initPos;
    bool m_moveForward;
    private bool m_rotating;
    [SerializeField]
    private Animator m_animator;
    private bool m_started;

    private void Start()
    {
    }

    public void StartGame()
    {
        m_started = true;
    }

    private void Update()
    {
        base.Update();
        if (!m_started)
            return;
        if (!m_startMoving)
            return;
        if (m_moveForward)
        {

            if (Vector3.Distance(transform.position, m_initPos + transform.right * m_moveDistance) > 0.005)
                m_rb.MovePosition(Vector3.MoveTowards(transform.position, m_initPos + transform.right * m_moveDistance, m_moveSpeed * Time.deltaTime));
            else if (!m_rotating)
                StartCoroutine(Rotate(false));
        }
        else
        {
            if (Vector3.Distance(transform.position, m_initPos + transform.right * m_moveDistance) > 0.005)
                m_rb.MovePosition(Vector3.MoveTowards(transform.position, m_initPos + transform.right * m_moveDistance, m_moveSpeed * Time.deltaTime));
            else if (!m_rotating)
                StartCoroutine(Rotate(true));
        }
    }

    IEnumerator Rotate(bool moveForward)
    {
        m_rotating = true;
        float timer = 0;
        while (timer <= m_stopTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Quaternion prevRot = transform.rotation;
        Vector3 frozenPos = transform.position;
        transform.Rotate(Vector3.forward, 180);
        Quaternion targetRot = transform.rotation;
        transform.rotation = prevRot;
        while (transform.rotation != targetRot)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, m_rotateSpeed * Time.deltaTime);
            transform.position = frozenPos;
            yield return null;
        }
        m_moveForward = moveForward;
        m_rotating = false;
    }

    public override void OnPlace()
    {
        m_startMoving = true;
        m_initPos = transform.position;
        m_moveForward = true;
        m_rb = GetComponent<Rigidbody>();
    }
}
