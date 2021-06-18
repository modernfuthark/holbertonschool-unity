using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
	/* Projectile positioning */
	private Vector2 startPos;
	private Vector2 endPos;

	/* RigidBody */
	private Rigidbody rb;

	/* GameManager */
	private GameManager manager;

    /* Audio */
    private AudioSource ding;

	// Start is called before the first frame update
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ding = GameObject.Find("ProjOrigin").GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Target")
		{
            ding.Play();
			Destroy(other.gameObject);
			manager.updateScore(10);
		}

		resetProjectile();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount == 0)
		{

		}
		if (!rb.useGravity)  // Not in motion
		{
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);

				switch (t.phase)
				{
					case TouchPhase.Began:

						startPos = new Vector2(t.position.x, t.position.y);
						break;

					case TouchPhase.Moved:
                        endPos = new Vector2(t.position.x, t.position.y);
						break;

					case TouchPhase.Ended:
						rb.useGravity = true;
						rb.isKinematic = false;
						endPos = new Vector2(t.position.x, t.position.y);
						rb.constraints = RigidbodyConstraints.None;
						Vector2 force = startPos - endPos;
						gameObject.transform.parent = null;
						rb.AddForce(new Vector3(-force.x, -force.y, -force.y) * .5f);
						manager.updateAmmo();
						break;
				}
			}
		}
		else
		{
			float fallLimit = PlaneManager.s_Plane.transform.position.y - 5f;
			if (gameObject.transform.position.y <= fallLimit)
				resetProjectile();
		}
	}

	void resetProjectile()
	{
		rb.isKinematic = true;
		rb.useGravity = false;
		gameObject.transform.parent = GameObject.Find("ProjOrigin").transform;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		gameObject.transform.localPosition = Vector3.zero;
	}
}
