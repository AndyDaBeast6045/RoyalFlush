using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void credits()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void settings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
