using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

	public GameObject[] monsterTemplates;
	private const int MonsterPoolSize = 30;
	private ArrayList[] activeMonsterInstances;
	private ArrayList[] inactiveMonsterInstances;
	// Use this for initialization
	void Start () {
		activeMonsterInstances = new ArrayList[monsterTemplates.Length];
		inactiveMonsterInstances = new ArrayList[monsterTemplates.Length];
		CreateMonsterPool ();
	}

	// Update is called once per frame
	void Update () {
		CollectInactiveMonster ();
	}

	void NewGroundDidCreate(GameObject ground) {
		Renderer groundRenderer = ground.GetComponent<Renderer> ();
		float width = groundRenderer.bounds.size.x;
		int monsterIndex = Random.Range (0, monsterTemplates.Length);
		float[] genPositions = new float[1];
		float value = Random.Range ( width / 3, width * 2 / 3);
		for (int i = 0; i < 1; ++i) {
			genPositions [i] = value + i * 1.2f;
		}
		for (int i = 0; i < genPositions.Length; ++i) {
			GameObject monster = GetMonsterFromPool (monsterIndex);
			if (monster != null) {
				Vector3 oldPos = monster.transform.position;
				monster.transform.position = new Vector3(ground.transform.position.x + genPositions[i], oldPos.y ,ground.transform.position.z);
			}
		}
	}

	void CreateMonsterPool() {
		for (int monsterIndex = 0; monsterIndex < monsterTemplates.Length; ++monsterIndex) {
			activeMonsterInstances[monsterIndex] = new ArrayList ();
			inactiveMonsterInstances[monsterIndex] = new ArrayList ();
			for (int i = 0; i < MonsterPoolSize; ++i) {
				GameObject monster = GameObject.Instantiate (monsterTemplates [monsterIndex]);
				monster.SetActive (false);
				inactiveMonsterInstances[monsterIndex].Add (monster);
			}
			monsterTemplates [monsterIndex].SetActive (false);
		}
	}

	void CollectInactiveMonster() {
		for (int monsterIndex = 0; monsterIndex < monsterTemplates.Length; ++monsterIndex) {
			for (int i = 0; i < activeMonsterInstances[monsterIndex].Count; ++i) {
				GameObject monster = activeMonsterInstances[monsterIndex] [i] as GameObject;
				Renderer monsterRenderer = monster.GetComponent<Renderer> ();
				if (Camera.main.WorldToScreenPoint (monsterRenderer.bounds.center + monsterRenderer.bounds.size / 2).x < 0.0) {
					Debug.Log ("Monster Out");
					inactiveMonsterInstances[monsterIndex].Add (monster);
					monster.SetActive (false);
					activeMonsterInstances[monsterIndex].RemoveAt (i);
					i--;
				}
			}
		}
	}

	GameObject GetMonsterFromPool(int monsterIndex) {
		if (inactiveMonsterInstances[monsterIndex].Count > 0) {
			GameObject monster = inactiveMonsterInstances[monsterIndex][0] as GameObject;
			inactiveMonsterInstances[monsterIndex].RemoveAt (0);
			activeMonsterInstances[monsterIndex].Add (monster);
			monster.SetActive (true);
			monster.SendMessage ("Reset");
			return monster;
		}
		return null;
	}

	public void Reset() {
		for (int monsterIndex = 0; monsterIndex < monsterTemplates.Length; ++monsterIndex) {
			for (int i = 0; i < activeMonsterInstances[monsterIndex].Count; ++i) {
				GameObject monster = activeMonsterInstances[monsterIndex] [i] as GameObject;
				monster.SetActive (false);
			}
			inactiveMonsterInstances[monsterIndex].AddRange (activeMonsterInstances[monsterIndex]);
			activeMonsterInstances[monsterIndex].RemoveRange (0, activeMonsterInstances[monsterIndex].Count);
		}

	}
}
