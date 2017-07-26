using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour {
	public PlayerController playerController;
	public MonsterController monsterController;
	public GroundController groundController;

	private bool gameRunning;
	// Use this for initialization
	void Start () {
		gameRunning = false;
		playerController.enabled = false;
		groundController.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerController.isDead) {
			GameFinish ();
		}
	}

	public void GameStart() {
		if (!gameRunning) {
			playerController.enabled = true;
			groundController.enabled = true;
			gameRunning = true;
		}
	}

	public void GameFinish() {
		if (gameRunning) {
			playerController.enabled = false;
			groundController.enabled = false;
			gameRunning = false;
			SendMessage ("GameOver");
		}
	}

	public void Reset() {
		playerController.Reset ();
		groundController.Reset ();
	}
}
