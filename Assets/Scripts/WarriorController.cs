using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour {

	public float speed;
	public float speedBullet;
	private int direction;
	private int moveX;
	private int moveY;
	private bool walking;
	private bool attacking;
	private Rigidbody2D rigidbody2D;
	private Animator animator;
	private Animator animatorGun;
	public GameObject bullet;
	public GameObject firePoint;
	public GameObject monster;
	public GameObject monsterNest;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		AddMonster ();

		direction = 2;
		attacking = false;
		walking = false;
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			attacking = true;
			Fire ();
		}

		if (Input.GetKeyUp (KeyCode.J)) {
			attacking = false;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			direction = 0;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			direction = 1;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			direction = 2;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			direction = 3;
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
			walking = true;
		} else {
			walking = false;
		}

		if (attacking) {
			walking = false;
		}

		animator.SetFloat ("direction", direction / 3f);
		animator.SetBool ("walking", walking);
		animator.SetBool ("attacking", attacking);

		moveX = 0;
		moveY = 0;

		if (walking) {
			if (direction == 0) {
				moveX = 0;
				moveY = 1;
			}

			if (direction == 1) {
				moveX = 1;
				moveY = 0;
			}

			if (direction == 2) {
				moveX = 0;
				moveY = -1;
			}

			if (direction == 3) {
				moveX = -1;
				moveY = 0;
			}
		}

		float velocityX = moveX * speed;
		float velocityY = moveY * speed;

		Vector2 newVelocity	= new Vector2 (velocityX, velocityY);

		rigidbody2D.velocity = newVelocity;
	}
		
	void Fire (){
		GameObject bulletCloned = Instantiate (bullet, firePoint.transform.position, transform.rotation	);	

		int bulletMoveX = 0;
		int bulletMoveY = 0;
		int bulletRotationDegrees = 0;

		if (direction == 0) {
			bulletMoveX = 0;
			bulletMoveY = 1;
			bulletRotationDegrees = 0;
		}

		if (direction == 1) {
			bulletMoveX = 1;
			bulletMoveY = 0;
			bulletRotationDegrees = -90;
		}

		if (direction == 2) {
			bulletMoveX = 0;
			bulletMoveY = -1;
			bulletRotationDegrees = 180;
		}

		if (direction == 3) {
			bulletMoveX = -1;
			bulletMoveY = 0;
			bulletRotationDegrees = 90;
		}

		float velocityX = bulletMoveX * speedBullet;
		float velocityY = bulletMoveY * speedBullet;

		Vector2 bulletVelocity = new Vector2 (velocityX, velocityY); 

		Rigidbody2D bulletRigidbody = bulletCloned.GetComponent<Rigidbody2D> ();
		bulletRigidbody.velocity = bulletVelocity;

//		// Rotation
		bulletCloned.transform.Rotate(0, 0, bulletRotationDegrees);
//
//		Debug.Log ("BulletRotation" + bulletCloned.transform.rotation);
	}

	IEnumerator AddMonsterAfterDelay(float delayInSeconds) {
		yield return new WaitForSeconds (delayInSeconds);

		AddMonster ();
	}

	void AddMonster () {
		Instantiate (monster, monsterNest.transform.position, Quaternion.identity);
		StartCoroutine (AddMonsterAfterDelay (Random.Range (1f, 10f)));
	}

}
