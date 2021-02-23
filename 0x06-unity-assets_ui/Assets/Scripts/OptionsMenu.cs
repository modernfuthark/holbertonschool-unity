using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
	private bool invert;

	public void Start()
	{
		if (PlayerPrefs.HasKey("InvertedY"))
		{
			var child = GameObject.Find("InvertYToggle");  // Start() doesnt like getting Toggle component directly
			child.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("InvertedY") != 0);
		}
	}
	public void Back()
	{
		string lastLevel = PlayerPrefs.GetString("LastScene") ?? "MainMenu";
		SceneManager.LoadScene(lastLevel);
	}

	public void Invert()
	{
		invert = gameObject.GetComponentInChildren<Toggle>().isOn;
	}

	public void Apply()
	{
		string lastLevel = PlayerPrefs.GetString("LastScene") ?? "MainMenu";
		PlayerPrefs.SetInt("InvertedY", invert ? 1 : 0);
		SceneManager.LoadScene(lastLevel);
	}
}
