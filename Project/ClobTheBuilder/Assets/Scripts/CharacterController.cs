using System.Collections;
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
    [SerializeField]
    private Vector2 m_wallJumpForce;

    private bool m_isGrounded;
    private float m_moveSpeed;
    private Rigidbody m_rb;
    private bool m_jumpQueued;
    private bool m_isSlidingLeft;
    private bool m_isSlidingRight;
    private BoxCollider m_col;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_col = GetComponent<BoxCollider>();
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.right * (m_col.bounds.extents.x * 1.1f), Vector3.right, out hit, 0.5f))
        {
            if (!hit.collider.isTrigger)
            {
                if (!m_isSlidingRight)
                    m_rb.velocity = Vector3.zero;
                m_moveSpeed = 0;
                if (!m_isGrounded)
                {
                    m_rb.velocity = Vector3.down * 0.1f;
                    m_isSlidingRight = true;
                }
                if (Input.GetButton("Left"))
                    transform.position += Vector3.left * 0.1f;
            }
        }
        else if (m_isSlidingRight)
            m_isSlidingRight = false;

        if (Physics.Raycast(transform.position + Vector3.left * m_col.bounds.extents.x * 1.1f, Vector3.left, out hit, 0.5f))
        {
            if (!hit.collider.isTrigger)
            {
                if (!m_isSlidingLeft)
                    m_rb.velocity = Vector3.zero;
                m_moveSpeed = 0;
                if (!m_isGrounded)
                {
                    m_rb.velocity = Vector3.down * 0.1f;
                    m_isSlidingLeft = true;
                }
            }
            if (Input.GetButton("Right"))
                transform.position += Vector3.right * 0.1f;
        }
        else if (m_isSlidingLeft)
            m_isSlidingLeft = false;
        m_rb.MovePosition(transform.position + Vector3.right * m_moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            if (m_isGrounded)
                Jump();
            if (m_isSlidingLeft)
            {
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(m_wallJumpForce, ForceMode.Impulse);
                m_jumpQueued = false;
            }
            else if (m_isSlidingRight)
            {
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(new Vector3(m_wallJumpForce.x * -1, m_wallJumpForce.y), ForceMode.Impulse);
                m_jumpQueued = false;
            }
            else
            {
                StopCoroutine(JumpQueueTimer());
                StartCoroutine(JumpQueueTimer());
            }
        }
    }

    private IEnumerator WallJumpRightLeway()
    {
        float timer = 0.2f;
        if (m_isGrounded)
            timer = 0;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        m_isSlidingRight = false;
    }

    private IEnumerator WallJumpLeftLeway()
    {
        float timer = 0.2f;
        if (m_isGrounded)
            timer = 0;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        m_isSlidingLeft = false;
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

    private void Jump()
    {
        m_rb.velocity = Vector3.zero;
        m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        m_isGrounded = true;
        if (m_jumpQueued)
        {
            Jump();
            m_jumpQueued = false;
            StopCoroutine(JumpQueueTimer());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_isGrounded = false;
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
