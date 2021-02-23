using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	private Vector3 playerV;
	private CharacterController playerCC;

	// Start is called before the first frame update
	void Start()
	{
		playerCC = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (playerCC.enabled == true)  // Just incase pausing causes an error?
		{
			bool groundedPlayer = playerCC.isGrounded;
			if (groundedPlayer && playerV.y < 0)
			{
				playerV.y = 0f;
			}
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			playerCC.Move(move * Time.deltaTime * speed);

			if (move != Vector3.zero)
				transform.forward = move;

			if (Input.GetKey(KeyCode.Space) && groundedPlayer)
			{
				playerV.y += Mathf.Sqrt(jumpHeight * -3.0f * -9.81f);
			}
			playerV.y += -9.81f * Time.deltaTime;
			playerCC.Move(playerV * Time.deltaTime);
		}
	}

	void LateUpdate()
	{
		if (transform.position.y < -10)
			transform.position = new Vector3(0, 20, 0);
	}

	/*void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && playerRB.velocity.y == 0)
		{
			playerRB.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
		}

		/*if (player.velocity.magnitude < 8)
		{
			if (Input.GetKey(KeyCode.W))
			{
				player.AddForce(0, 0, speed);
			}

			if (Input.GetKey(KeyCode.S))
			{
				player.AddForce(0, 0, speed * -1);
			}

			if (Input.GetKey(KeyCode.D))
			{
				player.AddForce(speed, 0, 0);
			}

			if (Input.GetKey(KeyCode.A))
			{
				player.AddForce(speed * -1, 0, 0);
			}
		}
	}*/
}
