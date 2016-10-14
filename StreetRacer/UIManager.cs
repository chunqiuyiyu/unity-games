using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text scoreText;
             int score;
             bool isGameOver;
             public Button[] buttons;

	void Start () {
	   score = 0;
                isGameOver = false;
                InvokeRepeating("ScoreUpdate", 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	   scoreText.text = "Score : " + score;
	}

            void ScoreUpdate(){
                if(!isGameOver){
                    score ++;
                }
            }

            public void GameOver(){
                isGameOver = true;
                foreach(Button btn in buttons){
                    btn.gameObject.SetActive(true);
                }
            }
            //switch the game scenes
            public void Play(){
                Time.timeScale = 1;
                Application.LoadLevel("Level1");
            }
            //pause or resume the game
            public void Pause(){
                if(Time.timeScale == 1){
                    Time.timeScale = 0;
                }else if(Time.timeScale == 0){
                    Time.timeScale = 1;
                }
            }

            public void Menu(){
                Application.LoadLevel("Menu");
            }

            public void Quit(){
                Application.Quit();
            }
}
