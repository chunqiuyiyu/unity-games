using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float carSpeed;
             Vector3 position;

	void Start () {
	   position = transform.position;
	}

	void Update () {
	   position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
                transform.position = position;
	}
}
