using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private float m_gameTime;
    [SerializeField]
    private FloatValue m_gameTimer;
    [SerializeField]
    private float m_increaseFromWater;
    [SerializeField]
    private float m_decreaseFromHeat;
    [SerializeField]
    private Renderer m_renderer;
    [SerializeField]
    private Material m_wetBoi;
    [SerializeField]
    private Material m_dryBoi;
    [SerializeField]
    private GameEvent m_timerEndEvent;

    private TimeLerper m_lerper = new TimeLerper();

    // Start is called before the first frame update
    private void OnEnable()
    {
        m_gameTimer.value = 0;
        m_lerper.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        m_gameTimer.value += Time.deltaTime;
        m_renderer.material.SetFloat("_Blend", m_gameTimer.value / m_gameTime);
        if (m_gameTimer.value >= m_gameTime)
            m_timerEndEvent.Invoke();
    }

    public void PauseGame()
    {

    }

    public void ResumeGame()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
            m_gameTimer.value += m_increaseFromWater;
        else if (other.CompareTag("Heat"))
            m_gameTimer.value -= m_decreaseFromHeat;
        if (m_gameTimer.value > m_gameTime)
            m_gameTimer.value = m_gameTime;
    }
}
