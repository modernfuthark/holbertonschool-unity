using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
	/* AR Planes */
	private ARRaycastManager m_RaycastManager;
	private ARPlaneManager planes;
	static List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
	public static ARPlane s_Plane;
	static bool planeFound = false;

	/* UI */
	public Text searchingText;
	public Text selectionText;
	public Button startButton;
    public Text scoreText;
    public Text ammoText;

    /* GameManager */
	public GameObject gameManager;

	// Start is called before the first frame update
	void Start()
	{
		s_Plane = null;
		m_RaycastManager = GetComponent<ARRaycastManager>();
		planes = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        planes.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (planes.trackables.count > 0 && !planeFound)
		{
			searchingText.gameObject.SetActive(false);
			selectionText.gameObject.SetActive(true);
		}
		if (Input.touchCount > 0 && !planeFound)
		{
			if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
			{

			}
			ARRaycastHit hit = m_Hits[0];

			if (hit.trackable is ARPlane plane)
			{
				if (s_Plane != null)
				{
					s_Plane.GetComponent<LineRenderer>().startColor = Color.black;
					s_Plane.GetComponent<LineRenderer>().endColor = Color.black;
				}
				s_Plane = planes.GetPlane(hit.trackableId);
				s_Plane.GetComponent<LineRenderer>().startColor = Color.green;
				s_Plane.GetComponent<LineRenderer>().endColor = Color.green;

				selectionText.gameObject.SetActive(false);
				startButton.gameObject.SetActive(true);
			}
			else
			{
				if (s_Plane != null)
				{
					s_Plane.GetComponent<LineRenderer>().startColor = Color.black;
					s_Plane.GetComponent<LineRenderer>().endColor = Color.black;
				}
				s_Plane = null;
				selectionText.gameObject.SetActive(true);
				startButton.gameObject.SetActive(false);
			}
		}
	}

	public void SelectPlane()
	{
		if (s_Plane != null)
		{
			planeFound = true;
			planes.enabled = false;

			foreach (var p in planes.trackables)
			{
				if (p.trackableId != s_Plane.trackableId)
					Destroy(p.gameObject);
			}

            ammoText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
			selectionText.gameObject.SetActive(false);
			startButton.gameObject.SetActive(false);
            s_Plane.GetComponent<LineRenderer>().enabled = false;
            s_Plane.GetComponent<MeshRenderer>().enabled = false;
			gameManager.SetActive(true);
		}
	}
}
