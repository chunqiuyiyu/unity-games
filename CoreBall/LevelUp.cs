using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour {

	public int level = 1;
	public GameObject[] levels;
	// Use this for initialization
	void Start () {
		//get level data
		level = PlayerPrefs.GetInt("coreball");
		if (level == 2) {
			//counterclockwise
			transform.GetComponent<Turn>().state = 2;
		}
		//generate the levels
		GameObject.Find("TCore").GetComponent<Text>().text = level.ToString();
		GameObject levelGameobjct = Instantiate(levels[level-1]) as GameObject;
		levelGameobjct.transform.parent = GameObject.Find ("Core").transform;

		//save the data
		if(!PlayerPrefs.HasKey("coreball")){
			PlayerPrefs.SetInt ("coreball", level);
		}
	}

	public void OnWin(){
		//speed up
		transform.GetComponent<Turn>().velocity += 10;

		if (level == 3) {
			PlayerPrefs.SetInt ("coreball", 1);
			SceneManager.LoadScene (0);
			return;
		}
		level++;
		PlayerPrefs.SetInt ("coreball", level);
		SceneManager.LoadScene (0);
	}
}
