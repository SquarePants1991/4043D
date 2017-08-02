using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDragon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Reset() {
		int level = Random.Range (1, 4);
		Vector3 oldPos = this.transform.position;
		oldPos.y = level;
		this.transform.position = oldPos;
	}
}
