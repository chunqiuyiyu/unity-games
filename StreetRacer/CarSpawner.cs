using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

	public GameObject[] cars;
             public float maxPos;

              float timer;
              Vector3 tmp;
              public float delayTimer = 0.5f;

	void Start () {
                tmp = transform.position;
                timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {
	   delayTimer -= Time.deltaTime;
                if(delayTimer <= 0){
                    //spawn the enemy car
                    Vector3 pos = new Vector3(Random.Range(-maxPos, maxPos), tmp.y, tmp.z);
                    int carNo = Random.Range(0, cars.Length);
                    Instantiate(cars[carNo], pos, transform.rotation);

                    delayTimer = timer;
                }
	}
}
