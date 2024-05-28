using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnLevelsButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void On1Button()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void On2Button()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void On3Button()
    {
        SceneManager.LoadScene("Level 3");
    }
}
