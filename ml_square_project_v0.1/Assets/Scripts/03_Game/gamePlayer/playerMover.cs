using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMover : MonoBehaviour {
	private bool isFacingRight = true;
	private bool isAbleToJump = true;
	private float walkSpeed = 5.5f;
	private bool isAbleToMove = true;
	private Rigidbody2D rb2d;
	private SpriteRenderer playerSprite;
	public List<Sprite> SpriteList;

	//プレイヤーのジャンプ
	public void jump(float jumpPower){
		GetComponent<Rigidbody2D> ().AddForce (Vector3.up * jumpPower * 10f);
	}

	//プレイヤーの方向変換
	public void changeFace(){
		if (isFacingRight) {
			isFacingRight = false;
		} else {
			isFacingRight = true;
		}
	}

	//プロパティ--------------------------------
	public bool IsAbleToJump{
		set{isAbleToJump = value;}
		get{return isAbleToJump;}
	}
	public float WalkSpeed{
		set{walkSpeed = value;}
		get{return walkSpeed;}
	}
	public bool IsAbleToMove{
		set{isAbleToMove = value;}
		get{return isAbleToMove;}
	}
	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		playerSprite = gameObject.transform.FindChild ("playerSprite").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAbleToMove) {
			if (isFacingRight) {
				rb2d.velocity = Vector3.right * walkSpeed;
				playerSprite.sprite = SpriteList [0];
			} else {
				rb2d.velocity = Vector3.right * -walkSpeed;
				playerSprite.sprite = SpriteList [1];
			}
		} else {
			rb2d.velocity = Vector3.right * 0f;
			if (isFacingRight) {
				playerSprite.sprite = SpriteList [2];
			} else {
				playerSprite.sprite = SpriteList [3];
			}
		}
	}
}
