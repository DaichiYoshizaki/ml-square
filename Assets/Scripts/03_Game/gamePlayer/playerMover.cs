using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMover : MonoBehaviour {
	private bool isFacingRight = true;
	public bool isAbleToJump = false;
	public float walkSpeed = 0.1f;
	private bool isAbleToMove = true;
	private Rigidbody2D rb2d;
	private SpriteRenderer playerSprite;
	public List<Sprite> SpriteList;
	public LayerMask groundLayer;
	public LayerMask wallLayer;
	private bool isHighJump = false;

	public bool IsHighJump{
		set{isHighJump = value;}
		get{return isHighJump;}
	}

	private bool IsGrounded(){
		bool isGround;
		isGround = Physics2D.Linecast (transform.position, transform.position - transform.up * 1.1f, groundLayer);
		return isGround;
	}

	private bool isWallCollied(){
		bool isWallCollied;
		if (isFacingRight) {
			isWallCollied = Physics2D.Linecast (transform.position, transform.position + transform.right * 1.4f, wallLayer);
		} else {
			isWallCollied = Physics2D.Linecast (transform.position, transform.position - transform.right * 1.4f, wallLayer);
		}
		return isWallCollied;
	}

	//プレイヤーのジャンプ
	public void jump(float jumpPower){
		if (!pauser.isPause) {
			if (isAbleToJump) {
				rb2d.AddForce (Vector2.up * jumpPower);
				isAbleToJump = false;

			}
		}
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

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		isAbleToJump = false;

		//着地していてるなら

		if (IsGrounded ()) {
			isAbleToJump = true;
		}

		//壁にぶつかった時

		if (isWallCollied ())
			changeFace ();

		//もし移動ができるならば

		Color col = playerSprite.color;

		if (isAbleToMove) {
			//右左でスプライトの変更
			if (isFacingRight) {
				//rb2d.velocity = Vector2.right * walkSpeed * Time.deltaTime;
				transform.Translate(Vector2.right * walkSpeed);
				playerSprite.sprite = SpriteList [0];
			} else {
				//rb2d.velocity = Vector2.right * -walkSpeed * Time.deltaTime;
				transform.Translate(Vector2.right * -walkSpeed);
				playerSprite.sprite = SpriteList [1];
			}
			if (col.g <= 1f) {
				col.g+=0.1f;
				col.b+=0.1f;
			}
			//左右でスプライトの
		} else {
			rb2d.velocity = Vector2.right * 0f;
			if (isFacingRight) {
				playerSprite.sprite = SpriteList [2];
			} else {
				playerSprite.sprite = SpriteList [3];
			}
			if (playerSprite.color.g >= 0f && isHighJump) {
				col.g-=0.1f;
				col.b-=0.1f;
			}
		}
		playerSprite.color = col;
	}
	public void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("item")){
			itemManager.GetItem();
			Destroy(col.gameObject);
		}
	}
}