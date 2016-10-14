using UnityEngine;
using System.Collections;

public class CarContraller : MonoBehaviour {

	public float carSpeed;
             public float maxPos;
             public UIManager ui;
             public AudioManager am;
             Vector3 position;
             bool isAndroid = false;
             Rigidbody2D rb;

             void Awake(){
                am.carSound.Play();

                rb = GetComponent<Rigidbody2D>();
                #if UNITY_ANDROID
                    isAndroid = true;
                #else
                    isAndroid = false;
                #endif
             }

	void Start () {
	   position = transform.position;
                Debug.Log(isAndroid);
	}

	void Update () {
        if(isAndroid){
            //android devices special controllers
            // TouchMove();
            AcceleMove();
        }else{
            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x , -maxPos, maxPos);
            transform.position = position;
        }

        //limit the car in track bonus
        position = transform.position;
        position.x = Mathf.Clamp(position.x , -maxPos, maxPos);
        transform.position = position;
	}

            //us touch to contral the  car
            void TouchMove(){
                if(Input.touchCount > 0){
                    Touch touch = Input.GetTouch(0);
                    float mid = Screen.width / 2;
                    if(touch.position.x < mid && touch.phase == TouchPhase.Began){
                        MoveLeft();
                    }else if(touch.position.x > mid && touch.phase == TouchPhase.Began){
                        MoveRight();
                    }
                }else{
                    StayZero();
                }
            }

            void OnCollisionEnter2D(Collision2D coll){
                if(coll.gameObject.tag == "Enemy"){
                    Destroy(gameObject);
                    ui.GameOver();
                    am.carSound.Stop();
                }
            }

            //Input.acceleration.y
            void AcceleMove(){
                float tmp = Input.acceleration.x;
                Debug.Log(tmp);
                if(tmp < -0.1f){
                    MoveLeft();
                }else if(tmp > 0.1f){
                    MoveRight();
                }else{
                    StayZero();
                }

            }

            public void MoveLeft(){
                rb.velocity = new Vector2(-carSpeed, 0);
            }

            public void MoveRight(){
                rb.velocity = new Vector2(carSpeed, 0);
            }

            public void StayZero(){
                rb.velocity = Vector2.zero;
            }
}
