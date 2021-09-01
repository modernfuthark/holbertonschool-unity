using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	private Vector3 playerV;
	private CharacterController playerCC;
	public Camera cam;
	private Animator anim;
	private int fallingTime = 0;

	// Start is called before the first frame update
	void Start()
	{
		playerCC = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
			anim.SetTrigger("Running");
		if (playerCC.enabled == true)  // Just incase pausing causes an error?
		{
			bool groundedPlayer = playerCC.isGrounded;
			if (groundedPlayer && playerV.y < 0)
			{
				playerV.y = 0f;
			}
			var forward = cam.transform.forward;
			forward.Normalize();
			Vector3 move = (cam.transform.forward  * Input.GetAxis("Vertical")) + (cam.transform.right * Input.GetAxis("Horizontal"));
			move = new Vector3(move.x, 0, move.z);
			playerCC.Move(move * Time.deltaTime * speed);


			if (move != Vector3.zero)
				transform.forward = move;

			if (Input.GetKey(KeyCode.Space) && groundedPlayer)
			{
				anim.SetTrigger("Jumping");
				playerV.y += Mathf.Sqrt(jumpHeight * -3.0f * -9.81f);
			}
			if (!groundedPlayer && playerV.y > 10)
			{
				anim.SetTrigger("Falling");
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
}
