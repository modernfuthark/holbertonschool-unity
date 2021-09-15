using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	private Vector3 playerV;
	private CharacterController playerCC;
	public Camera cam;
	private Animator anim;

	private PlayerInputs input;

	private Vector3 movement = new Vector3(0, 0, 0);


	private void Awake() => input = new PlayerInputs();

	// Start is called before the first frame update
	void Start()
	{
		playerCC = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();

		input.Enable();
	}

	// Update is called once per frame
	void Update()
	{
		input.Player.Move.performed += ctx =>
		{
			var v2 = ctx.ReadValue<Vector2>();
			movement = new Vector3(v2.x, 0, v2.y);
			movement.Normalize();
		};
		if (playerCC.enabled == true)  // Just incase pausing causes an error?
		{
			bool groundedPlayer = playerCC.isGrounded;
			if (groundedPlayer && playerV.y < 0)
			{
				playerV.y = 0f;
			}

			playerCC.Move(movement * Time.deltaTime * speed);


			if (movement != Vector3.zero)
				transform.forward = movement;

			input.Player.Jump.started += ctx =>
			{
				if (groundedPlayer && ctx.phase == InputActionPhase.Started)
				{
					Debug.Log("Got");
					anim.SetTrigger("Jumping");
					playerV.y += Mathf.Sqrt(jumpHeight * -100.0f * -9.81f);
				}
			};
			/*
			if (Keyboard.current.spaceKey.wasPressedThisFrame && groundedPlayer)
			{
				anim.SetTrigger("Jumping");
				playerV.y += Mathf.Sqrt(jumpHeight * -3.0f * -9.81f);
			}*/
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
