using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static int countReplay; 

    public void ReplayGame()
    {
        countReplay++;
        SceneManager.LoadScene("Level");
        if(countReplay >= 5)
        {
            countReplay = 0;
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
