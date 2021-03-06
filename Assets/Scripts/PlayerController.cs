﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public bool isDead;
	public int score;
	public int highestScore;
	public float speed;
	public int hardLevel;

	public float scoreF;
	public float originSpeed;
	private bool isOnGround;
	private Vector3 originPosition;
	private Animator animator;
	// Use this for initialization
	void Start () {
		isDead = false;
		originPosition = transform.position;
		highestScore = PlayerPrefs.GetInt ("HighestScore");
		originSpeed = speed;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false) {
			speed = originSpeed + hardLevel * 2;
			if (speed > 20) {
				speed = 20;
			}
			transform.Translate (Time.deltaTime * speed, 0, 0);
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
			animator.SetBool ("onGround", isOnGround);
		} else if (collision.gameObject.tag == "Monster") {
			PlayerPrefs.SetInt ("HighestScore", highestScore);
			isDead = true;
			animator.SetBool ("die", isDead);
		}
	}

	// Player Behaviors
	void Jump() {
		if (isOnGround) {
//			transform.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 400, 0));
			isOnGround = false;
			animator.SetBool ("onGround", isOnGround);
			transform.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 12, 0);
		}
	}


	// Reset to origin state
	public void Reset() {
		hardLevel = 0;
		speed = originSpeed;
		scoreF = 0;
		score = 0;
		highestScore = PlayerPrefs.GetInt ("HighestScore");
		isDead = false;
		animator.SetBool ("die", isDead);
		transform.position = originPosition;
		SyncCamera ();
	}
}
