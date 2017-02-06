using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = transform.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		line.SetPosition (1, transform.position);
	}
}
