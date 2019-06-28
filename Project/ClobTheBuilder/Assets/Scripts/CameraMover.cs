using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_preGamePos;
    [SerializeField]
    private Vector3 m_inGamePos;
    [SerializeField]
    private float m_moveSpeed;
    private bool m_moveToInGame;

    private void OnEnable()
    {
        transform.position = m_preGamePos;
    }

    public void StartGame()
    {
        m_moveToInGame = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_moveToInGame)
            return;
        transform.position = Vector3.MoveTowards(transform.position, m_inGamePos, m_moveSpeed * Time.deltaTime);
    }
}
