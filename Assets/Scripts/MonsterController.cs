﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
	
	public GameObject[] monsterTemplates;
	private const int MonsterPoolSize = 30;
	private ArrayList activeMonsterInstances;
	private ArrayList inactiveMonsterInstances;
	// Use this for initialization
	void Start () {
		CreateMonsterPool ();
	}
	
	// Update is called once per frame
	void Update () {
		CollectInactiveMonster ();
	}

	void NewGroundDidCreate(GameObject ground) {
		Renderer groundRenderer = ground.GetComponent<Renderer> ();
		float width = groundRenderer.bounds.size.x;
		float[] genPositions = new float[2];
		genPositions[0] = Random.Range (0, width / 2);
		genPositions[1] = Random.Range (0, -width / 2);
		for (int i = 0; i < genPositions.Length; ++i) {
			GameObject monster = GetMonsterFromPool ();
			Vector3 oldPos = monster.transform.position;
			monster.transform.position = new Vector3(ground.transform.position.x + genPositions[i], oldPos.y ,ground.transform.position.z);
		}
	}

	void CreateMonsterPool() {
		activeMonsterInstances = new ArrayList ();
		inactiveMonsterInstances = new ArrayList ();
		for (int i = 0; i < MonsterPoolSize; ++i) {
			GameObject monster = GameObject.Instantiate (monsterTemplates [0]);
			monster.SetActive (false);
			inactiveMonsterInstances.Add (monster);
		}
	}

	void CollectInactiveMonster() {
		for (int i = 0; i < activeMonsterInstances.Count; ++i) {
			GameObject monster = activeMonsterInstances [i] as GameObject;
			if (monster.GetComponent<Renderer> ().isVisible == false) {
				inactiveMonsterInstances.Add (monster);
				activeMonsterInstances.RemoveAt (i);
				i--;
			}
		}
	}

	GameObject GetMonsterFromPool() {
		GameObject monster = inactiveMonsterInstances [0] as GameObject;
		inactiveMonsterInstances.RemoveAt (0);
		monster.SetActive (true);
		return monster;
	}
}