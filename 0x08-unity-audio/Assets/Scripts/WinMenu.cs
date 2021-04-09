using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Match nextScene = Regex.Match(currentScene, @"(\d.*)$");
        int nextNum = Int32.Parse(nextScene.ToString()) + 1;
        string nextSceneName = $"Level{nextNum:D2}";
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
        else
            SceneManager.LoadScene("MainMenu");
    }
}
