using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

	public float speed;
	public int energy;
	private Rigidbody2D rigidbody2D;
	public Text monsterEnergyText;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
		ChangeDirection ();
		UpdateMonsterEnergyText ();
	}

	IEnumerator ChangeDirectionAfterDelay(float delayInSeconds) {
		yield return new WaitForSeconds (delayInSeconds);
		ChangeDirection ();
	}

	void ChangeDirection() {
		int direction = Random.Range (0, 5);

		float moveX = 0;
		float moveY = 0;

		if (direction == 1) {
			moveX = 0;
			moveY = -1;
		}

		if (direction == 2) {
			moveX = 1;
			moveY = 0;
		}

		if (direction == 3) {
			moveX = 0;
			moveY = 1;
		}

		if (direction == 4) {
			moveX = -1;
			moveY = 0;
		}

		float velocityX = moveX * speed;
		float velocityY = moveY * speed;

		animator.SetFloat ("VelocityX", velocityX);
		animator.SetFloat ("VelocityY", velocityY);

		if (Mathf.Abs(velocityX) > 0 || Mathf.Abs(velocityY) > 0 ) {
			animator.SetBool ("Walking", true);
		} else {
			animator.SetBool ("Walking", false);
		}

		Vector2 newVelocity	= new Vector2 (velocityX, velocityY);

		rigidbody2D.velocity = newVelocity;

		StartCoroutine(ChangeDirectionAfterDelay (Random.Range (0f, 3f)));
	}

	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log ("collision.gameObject.name: " + collision.gameObject.name);

		if (collision.gameObject.tag == "Bullet") {
			energy -= 1;

			UpdateMonsterEnergyText ();

			if (energy == 0) {
				Destroy (gameObject);
			}
		}
	}

	void UpdateMonsterEnergyText() {
		monsterEnergyText.text = "" + energy;
	}

		
}
