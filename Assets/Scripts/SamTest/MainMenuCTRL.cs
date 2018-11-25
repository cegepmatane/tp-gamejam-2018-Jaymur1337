using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCTRL : MonoBehaviour
{



    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void EditMapButton()
    {
        
    }

    public void ChoseMapButton()
    {
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
