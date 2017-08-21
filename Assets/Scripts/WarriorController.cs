using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour {

	public float speed;
	public float speedBullet;
	private Rigidbody2D rigidbody2D;
	private Animator animator;
	private Animator animatorGun;
	public GameObject gun;
	public GameObject bullet;
	public GameObject firePoint;
	public GameObject monster;
	public GameObject monsterNest;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		animatorGun = gun.GetComponent<Animator> ();

		AddMonster ();
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			animatorGun.SetBool ("Fire", true);	
			Fire ();
		}

		if (Input.GetKeyUp (KeyCode.J)) {
			animatorGun.SetBool ("Fire", false);	
		}
	}

	void FixedUpdate (){
		float moveX = 0;
		float moveY = 0;

		if (Input.GetKey(KeyCode.RightArrow)) {
			moveX = 1;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			moveX = -1;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			moveY = 1;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			moveY = -1;
		}


		float velocityX = moveX * speed;
		float velocityY = moveY * speed;

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
			animator.SetFloat ("velocityX", velocityX);
			animator.SetFloat ("velocityY", velocityY);
			animator.SetBool ("Walking", true);
		} else {
			animator.SetBool ("Walking", false);
		}

		Vector2 newVelocity	= new Vector2 (velocityX, velocityY);

		rigidbody2D.velocity = newVelocity;
	}

	void Fire (){
		GameObject bulletCloned = Instantiate (bullet, firePoint.transform.position, transform.rotation	);	
		Vector3 bulletForward = transform.rotation * (Vector3.up);   
		bulletCloned.GetComponent<Rigidbody2D> ().AddForce (bulletForward * speedBullet, ForceMode2D.Impulse);
	}

	IEnumerator AddMonsterAfterDelay(float delayInSeconds) {
		yield return new WaitForSeconds (delayInSeconds);

		AddMonster ();
	}

	void AddMonster () {
		GameObject monsterCloned = Instantiate (monster, monsterNest.transform.position, Quaternion.identity);
		StartCoroutine (AddMonsterAfterDelay (Random.Range (1f, 10f)));
	}

}
