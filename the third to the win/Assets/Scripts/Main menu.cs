using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(1);
    }
    public void GOtosettingsmenu()
    {
        SceneManager.LoadScene("settings menu");
    }
     public void GoToMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
