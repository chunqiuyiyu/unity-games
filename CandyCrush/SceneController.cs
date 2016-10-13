using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	//this script is usedf to switch scenes
	public void Play(){
		Application.LoadLevel("Main");
	}

	public void Quit(){
		Application.Quit();
	}
}
