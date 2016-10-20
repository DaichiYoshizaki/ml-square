using UnityEngine;
using System.Collections;

public class crane : MonoBehaviour {

	public GameObject player;
	private playerManager pm;
	private bool isEndCrane = false;
	float defaultHeight = 12f;
	bool isHitedPlayer = false;

	public bool IsEndCrane{
		set{ isEndCrane = value; }
		get{ return isEndCrane;}
	}
		
	// Use this for initialization
	void Start () {
		pm = player.GetComponent<playerManager>();
		isHitedPlayer = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer.IsTimeStop == true && !pm.IsDead) {
			if (!isHitedPlayer) {
				transform.position = new Vector3 (player.transform.position.x, defaultHeight, 0f);
				transform.Translate (-Vector3.up * Time.deltaTime * 5f);
			} else {
				transform.position = new Vector2 (player.transform.position.x, defaultHeight);
				transform.Translate (Vector2.up * Time.deltaTime * 5f);
				player.transform.Translate (Vector2.up * Time.deltaTime * 5f);
				if (transform.position.y > defaultHeight) {
					isEndCrane = true;
				}
			}
		}
	}
	public void OnTriggerEnter2D(Collider2D col){
		if (col == player)
			isHitedPlayer = true;
	}
}
