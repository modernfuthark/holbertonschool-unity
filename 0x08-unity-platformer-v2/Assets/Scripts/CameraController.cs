using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;

	public Vector3 offset;
	public float rotateSpeed = 5f;
	public float rotateX;
	public float rotateY;

	public bool isInverted = false;

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		
		offset = player.position - transform.position;

		isInverted = (PlayerPrefs.GetInt("InvertedY") != 0);
	}

	void Update()
	{
		rotateX += Input.GetAxis("Mouse X") * rotateSpeed;
		if (isInverted)
			rotateY -= Input.GetAxis("Mouse Y") * rotateSpeed;
		else
			rotateY += Input.GetAxis("Mouse Y") * rotateSpeed;

		rotateY = Mathf.Clamp(rotateY, -50, 50);
		Quaternion camRotate = Quaternion.Euler(-rotateY, rotateX, 0f);
		transform.position = player.transform.position - (camRotate * offset);
		transform.LookAt(player.position);
	}
}
