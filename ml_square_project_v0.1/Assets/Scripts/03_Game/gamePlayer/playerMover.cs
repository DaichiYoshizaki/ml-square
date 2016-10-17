using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMover : MonoBehaviour {
	private bool isFacingRight = true;
	private bool isAbleToJump;
	private float walkSpeed = 5.5f;
	private bool isAbleToMove;
	private Rigidbody2D rb2d;
	private SpriteRenderer playerSprite;
	public List<Sprite> SpriteList;

	//プレイヤーのジャンプ
	public void jump(float jumpPower){
		GetComponent<Rigidbody2D> ().AddForce (new Vector3(0f, jumpPower, 0f));
	}

	//プレイヤーの方向変換
	public void changeFace(){
		if (isFacingRight) {
			isFacingRight = false;
			playerSprite.sprite = SpriteList [1];
		} else {
			isFacingRight = true;
			playerSprite.sprite = SpriteList [0];
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
		if (isFacingRight) {
			rb2d.velocity = Vector3.right * walkSpeed;
		} else {
			rb2d.velocity = Vector3.right * -walkSpeed;
		}
	}
}
