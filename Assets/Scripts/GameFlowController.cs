using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour {
	public PlayerController playerController;
	public MonsterController monsterController;
	public GroundController groundController;

	private int hardLevel;

	private bool gameRunning;
	// Use this for initialization
	void Start () {
		gameRunning = false;
		playerController.enabled = false;
		groundController.enabled = false;
		hardLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			Debug.Log ("Game Running");
			if (playerController.isDead) {
				GameFinish ();
			}
			hardLevel = (int)(playerController.score / 100);
			monsterController.hardLevel = hardLevel;
			playerController.hardLevel = hardLevel;
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
		hardLevel = 0;
		playerController.Reset ();
		groundController.Reset ();
		monsterController.Reset ();
	}
}
