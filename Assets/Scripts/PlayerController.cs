using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public bool isDead;
	public int score;
	public int highestScore;

	public float scoreF;
	private bool isOnGround;
	private Vector3 originPosition;
	// Use this for initialization
	void Start () {
		isDead = false;
		originPosition = transform.position;
		highestScore = PlayerPrefs.GetInt ("HighestScore");
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			transform.Translate (Time.deltaTime * 10, 0, 0);
			scoreF += Time.deltaTime * 10;   
			score = (int)scoreF;
			highestScore = highestScore <= score ? score : highestScore;

			SyncCamera ();
			ControlFlow ();
		}
	}

	void SyncCamera() {
		Vector3 oldPosition = Camera.main.transform.position;
		oldPosition.x = transform.position.x + 6.0f;
		oldPosition.y = transform.position.y * 0.5f + 0.5f;
		Camera.main.transform.position = oldPosition;
		Vector3 playerPos = transform.position;
		playerPos.x = oldPosition.x;
		playerPos.y *= 0.9f;
		Camera.main.transform.LookAt (playerPos);
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
			PlayerPrefs.SetInt ("HighestScore", highestScore);
			isDead = true;
		}
	}

	// Player Behaviors
	void Jump() {
		if (isOnGround) {
//			transform.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 400, 0));
			isOnGround = false;
			transform.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 12, 0);
		}
	}


	// Reset to origin state
	public void Reset() {
		scoreF = 0;
		score = 0;
		highestScore = PlayerPrefs.GetInt ("HighestScore");
		isDead = false;
		transform.position = originPosition;
		SyncCamera ();
	}
}
