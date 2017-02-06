using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use the api of loading scene
using UnityEngine.SceneManagement;

public class Up : MonoBehaviour {

	public int upVelocity = 100;
	//true for move and false for stop
	bool stateSwitcher = true;
	public GameObject core;
	LineRenderer line;

	void Start(){
		core = GameObject.Find("Core");
		line = transform.GetComponent<LineRenderer> ();
	}

	void Update () {
		if (stateSwitcher) {
			transform.Translate (0, upVelocity * Time.deltaTime, 0);
			//the critical point
			if (transform.position.y >= -2.5f) {
				//stop the ball and set its parent: the core ball
				transform.position = new Vector3 (0, -2.5f, 0);
				transform.parent = core.transform;
				GameObject.Find ("Eye").GetComponent<AudioManager> ().hitAudioPlay ();
				stateSwitcher = false;
			}
		} else {
			//when the ball is stopped,draw the line to core
			line.SetPosition (1, transform.position);
		}
	}

	//chect the collision
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Ball")){
			core.GetComponent<Turn> ().OnStop ();
			//delay the 1s to restart
			Invoke("OnOver", 1.0f);
		}
	}

	void OnOver(){
		//restart the current scene
		SceneManager.LoadScene (0);
	}
}
