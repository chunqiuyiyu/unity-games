using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

	public int velocity = 1, state = 1;//1 for clockwise,2 for counterclockwise
	public bool switcher = true;

	// Update is called once per frame
	void Update () {
		//start to rotate
		if (switcher) {
			if (state == 1) {
				transform.Rotate (new Vector3 (0, 0, -velocity * Time.deltaTime));
			}
			if (state == 2) {
				transform.Rotate (new Vector3 (0, 0, velocity * Time.deltaTime));
			}
		}
	}

	void OnPlay(){
		switcher = true;
	}

	public void OnStop(){
		switcher = false;
	}

	public void OnReverse(){
		state = 2;
	}
}
