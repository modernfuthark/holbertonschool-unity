using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;

	private float mousex;
	private float mousey;
	private float cameray;
	private float camerax;
	private float lastplayery;
	private float lastplayerx;

	public bool isInverted = false;

	private Vector3 _offset;

	// Start is called before the first frame update
	void Start()
	{

		_offset = player.position - transform.position;

		cameray = transform.position.y;
		lastplayery = player.position.y;

		camerax = transform.position.x;
		lastplayerx = player.position.x;

        isInverted = (PlayerPrefs.GetInt("InvertedY") != 0);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 7);
        if (isInverted)
            mousey = Input.GetAxis("Mouse Y");
        else
		    mousey = -Input.GetAxis("Mouse Y");
		mousex = -Input.GetAxis("Mouse X");

		cameray += mousey;
		camerax += mousex;

		float playerY = player.position.y;
		cameray = Mathf.Clamp(cameray, playerY - 7, playerY + 7);
		cameray += (playerY - lastplayery) * 0.5f;
		lastplayery = playerY;

		float playerX = player.position.x;
		camerax = Mathf.Clamp(camerax, playerX - 7, playerX + 7);
		camerax += (playerX - lastplayerx) * 0.5f;
		lastplayerx = playerX;

		transform.position = new Vector3(camerax, cameray, transform.position.z);

		transform.LookAt(player.position);

		//player.rotation = transform.rotation;
	}
}
