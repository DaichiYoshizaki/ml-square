using UnityEngine;
using System.Collections;

public class jumpButton : MonoBehaviour {
	private bool push = false;
	private float jumpPower;
	public GameObject player;
	private playerMover playerMover;

	public void PushDown(){
		push = true;
	}

	public void PushUp(){
		push = false;
		if (jumpPower > 10f) {
			playerMover.jump (jumpPower);
		} else {			
			playerMover.jump (jumpPower * 0.5f);
		}
		jumpPower = 0f;
	}
	void Start(){
		playerMover = player.GetComponent<playerMover> ();
	}

	void Update(){
		if(push){
			jumpPower += 0.1f;
		}
	}
}
