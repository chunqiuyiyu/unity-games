using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stick : MonoBehaviour {


	public int speed = 5;
	public Texture[] AnimArr = new Texture[6];
	public GameObject over;
	public GameObject groundPrefab;

	GameObject stickControl, player, ground;
	int i = 0;
	float count = 0;
	bool rotateSwitcher = false, isTouchGround = false, isOver = false;
	Vector3 nextPos;
	Rigidbody rb;
	// initial status is 2,we can generete stick
	int status = 2;

	void Start () {
		stickControl = GameObject.Find("StickControl");
		player = GameObject.Find("Player");
		rb = player.gameObject.GetComponent<Rigidbody> ();
		nextPos = player.transform.position;
		ground = GameObject.Find ("GroundFirst");
	}

	void Update () {
		if (status == 2) {
			if (Input.GetMouseButton (0)) {
				stickControl.transform.localScale += new Vector3 (0, speed * Time.deltaTime, 0);
			}

			if (Input.GetMouseButtonUp (0)) {
				Invoke ("RotateSwitcher", 0.2f);
				status = 0;
			}
		}

		if (status == 0) {
			if (rotateSwitcher) {
				stickControl.transform.Rotate (new Vector3 (0, 0, -4.5f));
				count += 4.5f;
				if (count == 90) {
					// the stick is falling down
					Invoke("CheckStatus", 0.5f);
					rotateSwitcher = false;
				}
			}
		}
		if (status == 1) {
			PlayerAnim ();
			player.transform.position = Vector3.MoveTowards (player.transform.position,
				new Vector3(nextPos.x + ground.transform.localScale.x * 0.25f, player.transform.position.y, 0), Time.deltaTime * 5f);
			// move the stock to new ground position
			if (player.transform.position == new Vector3(nextPos.x + ground.transform.localScale.x * 0.25f, player.transform.position.y, 0)) {
				MoveStick ();
			}
		}

		if (status == -1) {
			if (isOver) {
				// click screen to reload scene
				if (Input.GetMouseButtonDown (0)) {
					SceneManager.LoadScene ("Main");
				}

				if (player.gameObject && player.gameObject.transform.position.y < -4.5f) {
					Destroy (player.gameObject);
				}
				return;
			}
			PlayerAnim ();
			player.transform.Translate (Time.deltaTime * 5f, 0, 0);

			// wolk follow the stick and drop down
			if (player.transform.position.x > stickControl.transform.localScale.y + nextPos.x + ground.transform.localScale.x * 0.25f) {
				rb.useGravity = true;
				isOver = true;
				Camera.main.gameObject.GetComponent<AudioManager> ().OndDiePlay();
				over.SetActive (true);
				// disconnect the carema
				Camera.main.transform.parent = null;
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Ground")) {
			// when player move to new ground, delete old ground
			Debug.Log (ground.transform.parent.gameObject.name);
			Destroy(ground.transform.parent.gameObject, 2f);

			nextPos = other.gameObject.transform.position;
			isTouchGround = true;
			ground = other.gameObject;

			GenerateGorund ();
			GetComponent<Score> ().getScore ();
		} else {
			isTouchGround = false;
		}

		// if stick is touching on red point, get score again
		if (other.gameObject.CompareTag("Point")) {
			GetComponent<Score> ().getScore ();
		}
	}

	void RotateSwitcher () {
		rotateSwitcher = true;
		Camera.main.gameObject.GetComponent<AudioManager> ().OnStickPlay ();
	}

	// check the player's status: -1 is dead, 1 is moveable
	void CheckStatus () {
		if (isTouchGround) {
			status = 1;
		} else {
			// game over
			status = -1;
		}
	}

	void GenerateGorund () {
		GameObject go = Instantiate (groundPrefab) as GameObject;
		go.transform.position = new Vector3 (nextPos.x + Random.Range (3, 4f), nextPos.y, 0);
		go.transform.FindChild ("Ground").localScale = new Vector3 (Random.Range (0.5f, 1), 1, 1);
	}

	void MoveStick () {
		stickControl.transform.position = ground.transform.FindChild ("Seat").position;
		stickControl.transform.rotation = Quaternion.identity;
		stickControl.transform.localScale = new Vector3 (1, 0.04f, 1);

		//restet the data of stick
		status = 2;
		count = 0;
	}

	// play the player's animation
	void PlayerAnim () {
		player.GetComponent<Renderer> ().material.mainTexture = AnimArr [i];
		// loop the texture
		i++;
		if (i == 6) {
			i = 0;
		}
	}
}
