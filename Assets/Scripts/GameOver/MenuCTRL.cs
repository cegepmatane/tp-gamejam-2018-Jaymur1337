using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCTRL : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("TestJak", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
     
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
