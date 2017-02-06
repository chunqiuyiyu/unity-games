using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip hitAudio;
	public AudioClip winAudio;
	AudioSource audioSource;

	void Start () {
		audioSource = transform.GetComponent<AudioSource> ();	
	}
	
	public void hitAudioPlay(){
		audioSource.clip = hitAudio;
		audioSource.Play ();
	}

	public void winAudioPlay(){
		audioSource.clip = winAudio;
		audioSource.Play ();
	}
}
