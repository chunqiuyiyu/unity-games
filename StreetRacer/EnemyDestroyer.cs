using UnityEngine;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {

            void OnCollisionEnter2D(Collision2D coll){
                if(coll.gameObject.tag == "Enemy"){
                    Destroy(coll.gameObject);
                }
            }
}
