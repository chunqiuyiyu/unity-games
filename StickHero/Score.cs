using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	GameObject score;
	int currentScore = 0;
	void Start () {
		score = GameObject.Find ("Score");
	}
	
	// Update is called once per frame
	void Update () {
		score.GetComponent<Text> ().text = currentScore.ToString ();
	}

	public void getScore(){
		Camera.main.gameObject.GetComponent<AudioManager> ().OnSorePlay ();
		currentScore++;
	}
}
