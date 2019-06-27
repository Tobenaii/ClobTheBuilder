using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float m_moveAcceleration;
    [SerializeField]
    private float m_stopAcceleration;
    [SerializeField]
    private float m_maxMoveSpeed;
    [SerializeField]
    private float m_jumpForce;

    private bool m_isGrounded;
    private float m_moveSpeed;
    private Rigidbody m_rb;
    private bool m_jumpQueued;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Right"))
        {
            m_moveSpeed += m_moveAcceleration * Time.deltaTime;
            if (m_moveSpeed < 0)
                m_moveSpeed += m_stopAcceleration * Time.deltaTime;
            if (m_moveSpeed >= m_maxMoveSpeed)
                m_moveSpeed = m_maxMoveSpeed;
        }
        else if (Input.GetButton("Left"))
        {
            m_moveSpeed -= m_moveAcceleration * Time.deltaTime;
            if (m_moveSpeed > 0)
                m_moveSpeed -= m_stopAcceleration * Time.deltaTime;
            if (m_moveSpeed <= m_maxMoveSpeed * -1)
                m_moveSpeed = m_maxMoveSpeed * -1;
        }
        else
            m_moveSpeed = Mathf.MoveTowards(m_moveSpeed, 0, m_stopAcceleration * Time.deltaTime);
        m_rb.MovePosition(transform.position + Vector3.right * m_moveSpeed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, 1, 1<<10))
        {
            m_isGrounded = true;
            if (m_jumpQueued)
            {
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                m_rb.velocity = Vector3.zero;
                StopCoroutine(JumpQueueTimer());
            }
        }
        else
        {
            m_isGrounded = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (m_isGrounded)
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            else
            {
                StopCoroutine(JumpQueueTimer());
                StartCoroutine(JumpQueueTimer());
            }
        }
    }

    private IEnumerator JumpQueueTimer()
    {
        m_jumpQueued = true;
        float timer = 1.0f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        m_jumpQueued = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().OnEnter(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().OnStay(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().OnExit(gameObject);
        }
    }
}
