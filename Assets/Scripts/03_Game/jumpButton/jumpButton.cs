﻿using UnityEngine;
using System.Collections;

public class jumpButton : MonoBehaviour {
	private bool push = false;
	private float jumpPower;
	private GameObject player;
	private playerMover playerMover;

	public void PushDown(){
		if (playerMover.IsAbleToJump) {
			push = true;
			playerMover.IsAbleToMove = false;
			SoundManager.Instance.PlaySE(0);
		}
	}

	public void PushUp(){
		if (playerMover.IsAbleToJump && push) {
			push = false;
			if (jumpPower > 30f) {
				playerMover.jump (2500f);
			} else {			
				playerMover.jump (1700f);
			}
			jumpPower = 0f;
			playerMover.IsAbleToMove = true;
			playerMover.IsAbleToJump = false;
			playerMover.IsHighJump = false;
		}
	}
	void Start(){
		player = GameObject.Find("gamePlayer");
		playerMover = player.GetComponent<playerMover> ();
	}

	void Update(){
		if(push){
			jumpPower += 1f;
			if (jumpPower > 30f) {
				playerMover.IsHighJump = true;
			}
		}
	}
}
