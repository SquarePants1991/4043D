using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	public GameFlowController gameFlowController;
	public Canvas gameStartPanel;
	public Canvas gameOverPanel;
	// Use this for initialization
	void Start () {
		gameStartPanel.enabled = true;
		gameOverPanel.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver() {
		Debug.Log("Game Over");
		gameStartPanel.enabled = false;
		gameOverPanel.enabled = true;
	}

	public void OnStartButtonClick() {
		gameFlowController.GameStart ();
		gameStartPanel.enabled = false;
	}

	public void OnRestartButtonClick() {
		gameOverPanel.enabled = false;
		gameFlowController.Reset ();
		gameFlowController.GameStart ();
	}
}
