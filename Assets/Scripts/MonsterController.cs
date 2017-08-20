using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

	public float speed;
	public int energy;
	private Rigidbody2D rigidbody2D;
	public Text monsterEnergyText;


	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		ChangeDirection ();
		UpdateMonsterEnergyText ();
	}

	IEnumerator ChangeDirectionAfterDelay(float delayInSeconds) {
		yield return new WaitForSeconds (delayInSeconds);
		ChangeDirection ();
	}

	void ChangeDirection() {
		float moveX = Random.Range (-1f, 1f);
		float moveY = Random.Range (-1f, 1f);

		float velocityX = moveX * speed;
		float velocityY = moveY * speed;

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
