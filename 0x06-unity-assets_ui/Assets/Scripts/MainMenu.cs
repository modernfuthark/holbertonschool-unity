using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int Level)
    {
        switch (Level)
        {
            case 1:
                Debug.Log("Load level 1");
                SceneManager.LoadScene("Level01");
                break;
            case 2:
                Debug.Log("Load level 2");
                SceneManager.LoadScene("Level02");
                break;
            case 3:
                Debug.Log("Load level 3");
                SceneManager.LoadScene("Level03");
                break;
            default:
                break;
        }
    }

    public void Options()
    {
        PlayerPrefs.SetString("LastScene", "MainMenu");
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
