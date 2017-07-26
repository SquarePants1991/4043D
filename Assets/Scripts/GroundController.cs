using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {
	public GameObject groundTemplate;
	public GameObject groundLeft;
	public GameObject groundRight;
	private GameObject currentLastObject;
	private GameObject[] grounds;

	// Use this for initialization
	void Start () {
		groundRight = GameObject.Instantiate (groundTemplate);
		groundLeft = GameObject.Instantiate (groundTemplate);
		Vector3 size = groundTemplate.transform.GetComponent<Renderer> ().bounds.size;
		groundLeft.transform.Translate (-size.x, 0, 0);
		groundRight.transform.Translate (size.x, 0, 0);

		currentLastObject = groundRight;
		grounds = new GameObject[3];
		grounds[0] = groundLeft;
		grounds[1] = groundRight;
		grounds[2] = groundTemplate;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject ground in grounds) {
			Renderer renderer = ground.transform.GetComponent<Renderer> ();
			Vector3 rightOnScreen = Camera.main.WorldToScreenPoint (renderer.bounds.center + renderer.bounds.size / 2);
			Vector3 leftOnScreen = Camera.main.WorldToScreenPoint (renderer.bounds.center - renderer.bounds.size / 2);
			if (rightOnScreen.x < -renderer.bounds.size.x) {
				Renderer currentLastObjectRenderer = currentLastObject.transform.GetComponent<Renderer> ();
				Vector3 size = currentLastObjectRenderer.bounds.size;
				ground.transform.Translate (currentLastObjectRenderer.bounds.center.x + size.x - ground.transform.position.x, 0, 0);
				currentLastObject = ground;

				SendMessage ("NewGroundDidCreate", ground);
			}
		}

	}

	public void Reset() {
		int index = 0;
		foreach (GameObject ground in grounds) {
			Renderer renderer = ground.transform.GetComponent<Renderer> ();
			ground.transform.position = new Vector3 (-renderer.bounds.size.x * (1 - index), ground.transform.position.y, ground.transform.position.z);
			index++;
		}
		Debug.Log ("Reset Ground");
		currentLastObject = grounds [2];
	}
}
