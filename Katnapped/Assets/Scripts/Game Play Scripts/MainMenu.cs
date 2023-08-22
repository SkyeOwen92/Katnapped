using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool gameHasEnded = false;
    public void PlayGame()
    {
        SceneManager.LoadScene("lvl1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
