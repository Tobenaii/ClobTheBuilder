using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Menu holders
    public GameObject mainMenuHolder;
    public GameObject levelSelectHolder;


    public void Awake()
    {
        mainMenuHolder.SetActive(true);
        levelSelectHolder.SetActive(false);
    }

    public void Play()
    {
        mainMenuHolder.SetActive(false);
        levelSelectHolder.SetActive(true);
    }

    public void Back()
    {
        mainMenuHolder.SetActive(true);
        levelSelectHolder.SetActive(false);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void Level2()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void Level3()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
