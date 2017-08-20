using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour {

	private Text text;
	public GameObject bulletExplosion;

	// Use this for initialization
	void Start () {
		Debug.Log ("BulletController.Start");
		text = GameObject.Find ("Text").GetComponent<Text>(); 
		text.text = "Changed!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter2D(Collider2D hit){
//
//		text.text = "Here!";
//		
//		Debug.Log ("bullet collision 1: ");
//		Debug.Log ("bullet collision: " + hit);
//
//		Instantiate (bulletExplosion, transform.position, Quaternion.identity);
//		Destroy (gameObject);
//	}
//
	void OnCollisionEnter2D(Collision2D collision){

		text.text = "Here Collision!";

		Debug.Log ("bullet collision 1: ");
		Debug.Log ("bullet collision: " + collision);

		GameObject bulletExplosionCloned = Instantiate (bulletExplosion, transform.position, Quaternion.identity);
	
		Destroy (gameObject);
		Destroy (bulletExplosionCloned, 2.0f);


	}

}
