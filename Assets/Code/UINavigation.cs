using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        if(RankedMode.isInRankedMode)
        {
            RankedMode.EnableRankedMode(false);
            RankedMode.SaveStats();
        }

        RankedMode.statsDisplayed = false;
        
        SceneManager.LoadScene("MainMenu");
    }

    public void MaxLevel()
    {
        if (!(SceneManager.sceneCountInBuildSettings <= PlayerPrefs.GetInt("MaxReachedLevel", 1)))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("UnlockedLevel", 1));
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("UnlockedLevel", 1) - 1);
        }
        
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            
            MainMenu();
        }


    }

    public void LoadLevel(int id)
    {
        if(PlayerPrefs.GetInt("UnlockedLevel", 1) >= id)
        {
            SceneManager.LoadScene("Level " + id);
        }
        
    }
}
