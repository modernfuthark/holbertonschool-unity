using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	/* Targets */
	public int TargetCount = 5;
	public GameObject target;

	/* Ammo */
	public static int ammo = 7;
	public GameObject proj;
	private GameObject spawnedProj = null;

	/* Score */
	public static int score = 0;
	private int[] scores = new int[4];
	public Image scoresBg;
	public Text first;
	public Text second;
	public Text third;


	/* UI */
	public Text scoreText;
	public Text ammoText;
	public Button replayButton;

	void Start()
	{
		// Score section
		scores[0] = PlayerPrefs.GetInt("First", 0);
		scores[1] = PlayerPrefs.GetInt("Second", 0);
		scores[2] = PlayerPrefs.GetInt("Third", 0);
		scores[3] = 0;

		scoreText.text = $"Score\n{score}";
		ammoText.text = $"Ammo\n{ammo}";
		spawnedProj = Instantiate(proj, Vector3.zero, Quaternion.identity);
		spawnedProj.transform.parent = GameObject.Find("ProjOrigin").GetComponent<Transform>();
		spawnedProj.transform.localPosition = Vector3.zero;
		createTargets();
	}

	/* Button Functions */
	public void restart()
	{
		if (replayButton.gameObject.activeSelf)
			saveScores();
		SceneManager.LoadScene("ARSlingshotGame");
	}

	public void quit()
	{
		if (replayButton.gameObject.activeSelf)
			saveScores();
		Application.Quit();
	}

	/* Game States */
	void gameEnd()
	{
		Destroy(spawnedProj.gameObject);
		saveScores(); PlayerPrefs.GetInt("First", 0);
		scoresBg.gameObject.SetActive(true);
		first.text = $"1st: {PlayerPrefs.GetInt("First", 0)}";
		second.text = $"2nd: {PlayerPrefs.GetInt("Second", 0)}";
		third.text = $"3rd: {PlayerPrefs.GetInt("Third", 0)}";
		spawnedProj = null;
		replayButton.gameObject.SetActive(true);
	}

	public void replay()
	{
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject T in targets)
        {
            Destroy(T.gameObject);
        }

		replayButton.gameObject.SetActive(false);
		scoresBg.gameObject.SetActive(false);
		score = 0;
		ammo = 7;
		Start();
	}

	/* Create Targets */
	void createTargets()
	{
		MeshCollider planeMesh = PlaneManager.s_Plane.GetComponent<MeshCollider>();
        planeMesh.enabled = true;
		for (int i = 0; i < TargetCount; i++)
		{
			Vector3 randLoc = new Vector3();
			randLoc.x = UnityEngine.Random.Range(planeMesh.bounds.min.x, planeMesh.bounds.max.x);
			randLoc.y = planeMesh.bounds.center.y + ((target.transform.localScale.y / 2) + .04f);
			randLoc.z = UnityEngine.Random.Range(planeMesh.bounds.min.z, planeMesh.bounds.max.z);

            GameObject clone = Instantiate(target, randLoc, Quaternion.identity);

			clone.transform.position = randLoc;
		}
        planeMesh.enabled = false;
	}

	/* Value Updaters */
	public void updateScore(int change)
	{
		score += change;
		scoreText.text = $"Score\n{score}";

		if (score >= (10 * TargetCount))
		{
			gameEnd();
		}
	}

	public void updateAmmo()
	{
		ammo--;
		ammoText.text = $"Ammo\n{ammo}";

		if (ammo == 0)
		{
            StartCoroutine(waitForProj());
		}
	}

    IEnumerator waitForProj()  // Waits for the projectile to finish flying
    {
        yield return new WaitUntil(() => spawnedProj.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll);
        gameEnd();
    }

	/* High scores */
	void saveScores()
	{
		scores[3] = score;
		Array.Sort(scores);
		Array.Reverse(scores);

		PlayerPrefs.SetInt("First", scores[0]);
		PlayerPrefs.SetInt("Second", scores[1]);
		PlayerPrefs.SetInt("Third", scores[2]);

		PlayerPrefs.Save();
	}
}
