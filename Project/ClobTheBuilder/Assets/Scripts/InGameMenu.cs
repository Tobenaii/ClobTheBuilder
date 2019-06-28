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

    public void ShowDriedOut()
    {
        m_gameOverMenu.SetActive(true);
        m_gameOverText.text = m_driedOutText;
    }
    public void ShowDead()
    {
        m_gameOverMenu.SetActive(true);
        m_gameOverText.text = m_choppedText;
    }

    public void ShowComplete()
    {
        m_levelCompleteMenu.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
