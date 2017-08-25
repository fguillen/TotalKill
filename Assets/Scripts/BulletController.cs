using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour {

	public GameObject bulletExplosion;

	void OnCollisionEnter2D(Collision2D collision){
		GameObject bulletExplosionCloned = Instantiate (bulletExplosion, transform.position, Quaternion.identity);
	
		Destroy (gameObject);
		Destroy (bulletExplosionCloned, 2.0f);
	}

}
