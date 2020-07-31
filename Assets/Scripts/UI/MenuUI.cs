using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{    
    public void StartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
