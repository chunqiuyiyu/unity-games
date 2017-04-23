using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip score, stick, die;
	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	public void OnSorePlay (){
		audioSource.clip = score;
		audioSource.volume = 0.3f;
		audioSource.Play ();
	}

	public void OnStickPlay (){
		audioSource.clip = stick;
		audioSource.volume = 0.7f;
		audioSource.Play ();
	}

	public void OndDiePlay (){
		audioSource.clip = die;
		audioSource.volume = 1.0f;
		audioSource.Play ();
	}
}
