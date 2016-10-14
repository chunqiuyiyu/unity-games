using UnityEngine;
using System.Collections;

public class TrackMove : MonoBehaviour {

            public float  speed = 0.5f;
            Vector2 offset;

	void Update () {
	   offset = new Vector2(0, Time.time * speed);
                GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
