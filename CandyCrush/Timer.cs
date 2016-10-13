using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timer = 20.0f;
	public Slider slider;
	public Text score;

	public GameObject gameover;
	public GameObject[] mainPanel;
	public GameController gc;

	float initTimer = 20.0f;

	void Start () {
		timer = initTimer;
	}
	
	void Update () {
		timer -= Time.deltaTime;
		slider.value = timer;

		if(timer <= 0){
			//Game over
			score.text = gc.allScore.ToString();
			foreach(GameObject go in mainPanel){
				go.SetActive(false);
			}
			gameover.SetActive(true);
		}
	}

	public void Reset(){
		timer = initTimer;
	}
}
