using UnityEngine;
using System.Collections;

public class jumpButton : MonoBehaviour {
	private bool push = false;
	private float jumpPower;
	public GameObject player;
	private playerMover playerMover;

	public void PushDown(){
		push = true;
		playerMover.IsAbleToMove = false;
	}

	public void PushUp(){
		push = false;
		if (jumpPower > 10f) {
			playerMover.jump (1000f);
		} else {			
			playerMover.jump (500f);
		}
		jumpPower = 0f;
		playerMover.IsAbleToMove = true;
		playerMover.IsAbleToJump = false;
	}
	void Start(){
		playerMover = player.GetComponent<playerMover> ();
	}

	void Update(){
		if(push){
			jumpPower += 1f;
		}
	}
}
