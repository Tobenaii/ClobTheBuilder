using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameOverMenu;
    [SerializeField]
    private GameObject m_levelCompleteMenu;
    [SerializeField]
    private Text m_gameOverText;
    [SerializeField]
    private string m_driedOutText;
    [SerializeField]
    private string m_choppedText;
    [SerializeField]
    private GameObject m_pauseMenu;
    [SerializeField]
    private GameEvent m_startGameEvent;
    [SerializeField]
    private GameObject m_startButton;

    public void ShowDriedOut()
    {
        m_gameOverMenu.SetActive(true);
        m_gameOverText.text = m_driedOutText;
        Time.timeScale = 0;
    }
    public void ShowDead()
    {
        m_gameOverMenu.SetActive(true);
        m_gameOverText.text = m_choppedText;
        Time.timeScale = 0;
    }

    public void ShowComplete()
    {
        m_levelCompleteMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowPause()
    {
        //m_pauseEvent.Invoke();
        Time.timeScale = 0;
        m_pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        m_pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        m_startGameEvent.Invoke();
        m_startButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Time.timeScale = 1;
        m_startButton.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ShowPause();
        }
    }
}
