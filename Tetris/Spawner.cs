using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	GameObject [] tetrisObj;
	void Start () {
		SpawnRandom ();
	}

	public void SpawnRandom() {
		int index = Random.Range (0, tetrisObj.Length);
		Instantiate (tetrisObj [index], transform.position, Quaternion.identity);
	}
}
