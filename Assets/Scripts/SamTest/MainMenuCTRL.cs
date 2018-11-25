using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCTRL : MonoBehaviour
{



    public void PlayButton()
    {
        SceneManager.LoadScene("TestJak", LoadSceneMode.Single);
    }

    public void EditMapButton()
    {
        SceneManager.LoadScene("Editor", LoadSceneMode.Single);
    }

    public void ChoseMapButton()
    {
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
