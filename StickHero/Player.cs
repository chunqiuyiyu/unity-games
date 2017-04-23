using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// scroll the bg
	GameObject BG1, BG2;
	void Start () {
		BG1 = GameObject.Find ("BG1");
		BG2 = GameObject.Find ("BG2");
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.name == "Hit1") {
			BG1.transform.Translate (24, 0, 0);
		}

		if (other.gameObject.name == "Hit2") {
			BG2.transform.Translate (24, 0, 0);
		}
	}
}
