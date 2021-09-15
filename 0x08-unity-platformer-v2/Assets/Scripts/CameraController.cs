using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	public Transform player;

	public Vector3 offset;
	public float rotateSpeed = 5f;
	public float rotateX;
	public float rotateY;

	private PlayerInputs input;

	public bool isInverted = false;


	private void Awake() => input = new PlayerInputs();
	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		offset = player.position - transform.position;

		isInverted = (PlayerPrefs.GetInt("InvertedY") != 0);
	}

	void Update()
	{
		var m = Mouse.current.delta.ReadValue();
		rotateX += m.x * rotateSpeed;
		if (isInverted)
			rotateY -= m.y * rotateSpeed;
		else
			rotateY += m.y * rotateSpeed;

		rotateY = Mathf.Clamp(rotateY, -50, 50);
		Quaternion camRotate = Quaternion.Euler(-rotateY, rotateX, 0f);
		transform.position = player.transform.position - (camRotate * offset);
		transform.LookAt(player.position);
	}
}
