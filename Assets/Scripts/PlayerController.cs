using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public bool isDead;
	private bool isOnGround;
	private Vector3 originPosition;
	// Use this for initialization
	void Start () {
		isDead = false;
		originPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			transform.Translate (Time.deltaTime * 10, 0, 0);
			SyncCamera ();

			ControlFlow ();
		}
	}

	void SyncCamera() {
		Vector3 oldPosition = Camera.main.transform.position;
		oldPosition.x = transform.position.x;
		Camera.main.transform.position = oldPosition;
	}

	void ControlFlow () {
		if (Input.GetKey (KeyCode.Space)) {
			Jump ();
		}		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ground") {
			isOnGround = true;
		} else if (collision.gameObject.tag == "Monster") {
			isDead = true;
		}
	}

	// Player Behaviors
	void Jump() {
		if (isOnGround) {
//			transform.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 400, 0));
			isOnGround = false;
			transform.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 6, 0);
		}
	}


	// Reset to origin state
	public void Reset() {
		isDead = false;
		transform.position = originPosition;
		SyncCamera ();
	}
}
