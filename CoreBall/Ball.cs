using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public GameObject ball;
	public int number = 20;
	bool loadSwitcher = true;

	// Update is called once per frame
	void Update () {
		if (number > 0) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				GameObject bullet = Instantiate (ball) as GameObject;
				number--;
			}
		} else {
			//add a switch to avoid loading scene repeatly because this code
			//is in update function
			if (loadSwitcher) {
				GameObject.Find ("Eye").GetComponent<AudioManager> ().winAudioPlay ();
				Invoke ("OnFinish", 1.5f);
				loadSwitcher = false;
			}
		}

		//show the rest of ball
		for(int i = 1; i<=3; i++){
			GameObject.Find ("T" + i).GetComponent<Text> ().text = (number + 1 - i).ToString ();
		}

		//when the number is negative,hide the bottomball
		if(number <= 2){
			if (GameObject.Find ("B" + number)) {
				GameObject.Find ("B" + number).SetActive (false);
			}
		}
	}

	//this level is finished
	void OnFinish(){
		transform.GetComponent<LevelUp> ().OnWin ();
	}
}
