using UnityEngine;
using System.Collections;

public class crane : MonoBehaviour {

	private GameObject dummyPlayer;
	private playerManager pm;
	private bool isStartCrane = false;
	private bool isEndCrane = false;
	float defaultHeight = 12f;
	bool isHitedPlayer = false;
	private bool isFindDummyPlayer = false;

	public bool IsStartCrane{
		set{ isStartCrane = value; }
		get{return isStartCrane;}
	}

	public bool IsEndCrane{
		set{ isEndCrane = value; }
		get{ return isEndCrane;}
	}
		
	// Use this for initialization
	void Awake() {
		dummyPlayer = null;
		isFindDummyPlayer = false;
		isEndCrane = false;
		isStartCrane = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer.IsTimeStop == true && isStartCrane) {
			if (!isFindDummyPlayer) {
				dummyPlayer = GameObject.Find ("dummyPlayer");
				isFindDummyPlayer = true;
			}
			if (!isHitedPlayer) {
				transform.position = new Vector3 (dummyPlayer.transform.position.x, transform.position.y, 1f);
				transform.Translate (-Vector3.up * Time.deltaTime * 5f);
			} else {
				transform.position = new Vector3 (dummyPlayer.transform.position.x, transform.position.y, 1f);
				transform.Translate (Vector2.up * Time.deltaTime * 5f);
				dummyPlayer.transform.Translate (Vector2.up * Time.deltaTime * 5f);
				if (transform.position.y > defaultHeight) {
					isEndCrane = true;
					isStartCrane = false;
					isHitedPlayer = false;
					Destroy (dummyPlayer);
					isFindDummyPlayer = false;
				}
			}
		}
	}
	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == dummyPlayer)
			isHitedPlayer = true;
	}
}
