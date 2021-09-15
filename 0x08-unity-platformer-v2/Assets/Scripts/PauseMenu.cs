using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;

    public Camera cam;
    Timer script;

    void Start()
    {
        script = gameObject.GetComponent<Timer>();
    }

	void Update()
	{
		if (Keyboard.current.escapeKey.IsPressed())
		{
			if (pauseCanvas.gameObject.activeSelf == false)
				Pause();
			else
				Resume();
		}
	}

	public void Pause()
	{
        script.CancelInvoke();
        gameObject.GetComponent<PlayerController>().enabled = false;
        cam.GetComponent<CameraController>().enabled = false;
        pauseCanvas.gameObject.SetActive(true);
	}

	public void Resume()
	{
        script.InvokeRepeating("CountUp", 0, .01f);
        gameObject.GetComponent<PlayerController>().enabled = true;
        cam.GetComponent<CameraController>().enabled = true;
		pauseCanvas.gameObject.SetActive(false);
	}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }
}
