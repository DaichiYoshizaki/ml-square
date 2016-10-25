using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {
	private bool isDead = false;
	// Use this for initialization
	void Start () {
		isDead = false;
	}
	public bool IsDead{
		set{ isDead = value;}
		get{ return isDead;}
	}
	public void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "enemy") {
			isDead = true;
			gameOverManager.open ();
		}
	}
	public void OnCollisionEnter2D(Collision2D col){
		if (col.transform.CompareTag("enemy")) {
			isDead = true;
			gameOverManager.open ();
		}
	}
}
