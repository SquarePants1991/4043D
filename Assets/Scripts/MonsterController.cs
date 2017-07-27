using System.Collections;
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
			if (monster != null) {
				Vector3 oldPos = monster.transform.position;
				monster.transform.position = new Vector3(ground.transform.position.x + genPositions[i], oldPos.y ,ground.transform.position.z);
			}
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
		monsterTemplates [0].SetActive (false);
	}

	void CollectInactiveMonster() {
		for (int i = 0; i < activeMonsterInstances.Count; ++i) {
			GameObject monster = activeMonsterInstances [i] as GameObject;
			Renderer monsterRenderer = monster.GetComponent<Renderer> ();
			if (Camera.main.WorldToScreenPoint(monsterRenderer.bounds.center + monsterRenderer.bounds.size / 2).x < 0.0) {
				Debug.LogError ("Monster Out");
				inactiveMonsterInstances.Add (monster);
				monster.SetActive (false);
				activeMonsterInstances.RemoveAt (i);
				i--;
			}
		}
	}

	GameObject GetMonsterFromPool() {
		if (inactiveMonsterInstances.Count > 0) {
			GameObject monster = inactiveMonsterInstances [0] as GameObject;
			inactiveMonsterInstances.RemoveAt (0);
			activeMonsterInstances.Add (monster);
			monster.SetActive (true);
			return monster;
		}
		return null;
	}

	public void Reset() {
		for (int i = 0; i < activeMonsterInstances.Count; ++i) {
			GameObject monster = activeMonsterInstances [i] as GameObject;
			monster.SetActive (false);
		}
		inactiveMonsterInstances.AddRange (activeMonsterInstances);
		activeMonsterInstances.RemoveRange (0, activeMonsterInstances.Count);

	}
}
