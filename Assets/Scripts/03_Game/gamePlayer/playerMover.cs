using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMover : MonoBehaviour {
	private bool isFacingRight = true;
	public bool isAbleToJump = false;
	public float walkSpeed = 0.1f;
	public bool isAwake = false;
	private bool isAbleToMove = true;
	private Rigidbody2D rb2d;
	private SpriteRenderer playerSprite;
	public List<Sprite> SpriteList;
	public LayerMask groundLayer;
	public LayerMask wallLayer;
	private bool isHighJump = false;
	private Collider2D col;

	public bool IsAwake{
		set{ isAwake = value; }
		get{ return isAwake; }
	}

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
			isWallCollied = Physics2D.Linecast (transform.position, transform.position + transform.right * col.bounds.size.x / 2f, wallLayer);
		} else {
			isWallCollied = Physics2D.Linecast (transform.position, transform.position - transform.right * col.bounds.size.x / 2f, wallLayer);
		}
		return isWallCollied;
	}

	//プレイヤーのジャンプ
	public void jump(float jumpPower){
		if (!pauser.isPause) {
			if (isAbleToJump) {
				rb2d.AddForce (Vector2.up * jumpPower);
				isAbleToJump = false;
				SoundManager.Instance.PlaySE(1);
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


	// Use this for initializations
	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		playerSprite = gameObject.transform.FindChild ("playerSprite").GetComponent<SpriteRenderer>();
		col = GetComponent<BoxCollider2D> ();
		isFacingRight = true;
		isAbleToMove = true;
		isHighJump = false;

	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isAwake) {
			Start();
			rb2d.velocity = new Vector3 (0f, 0f, 0f);
			isAwake = false;

		}

		isAbleToJump = false;

		//着地していてるなら

		if (IsGrounded ()) {
			isAbleToJump = true;
		} else {
			isAbleToMove = true;
		}

		//壁にぶつかった時

		if (isWallCollied ()) {
			changeFace ();
			SoundManager.Instance.PlaySE (3);
		}

		//もし移動ができるならば

		Color col = playerSprite.color;

		if (isAbleToMove) {
			//右左でスプライトの変更
			if (isFacingRight) {
				transform.Translate(Vector2.right * walkSpeed * Time.deltaTime * 50);
				playerSprite.sprite = SpriteList [0];
			} else {
				transform.Translate(Vector2.right * -walkSpeed * Time.deltaTime * 50);
				playerSprite.sprite = SpriteList [1];
			}
			if (col.g <= 1f) {
				col.g+=0.1f * Time.deltaTime * 50f;
				col.b+=0.1f * Time.deltaTime * 50f;
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
				col.g-=0.1f * Time.deltaTime * 50f;
				col.b-=0.1f * Time.deltaTime * 50f;
			}
		}
		playerSprite.color = col;
	}
	public void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("item")){
			itemManager.GetItem();
			SoundManager.Instance.PlaySE (4);
			Destroy(col.gameObject);
		}
		if (col.CompareTag ("enemy")) {
			pauser.Pause ();
			if (isFacingRight) {
				playerSprite.sprite = SpriteList [5];
			} else {
				playerSprite.sprite = SpriteList [4];
			}
		}
	}
	public void playerVisualReset(){
		playerSprite.sprite = SpriteList [0];
		transform.rotation = Quaternion.Euler (0, 0, 0);
		playerSprite.color = new Color (1f, 1f, 1f);
	}

	public void OnCollisionEnter2D(Collision2D col){
		if (col.transform.CompareTag ("enemy")) {
			SoundManager.Instance.PlaySE (2);
			pauser.Pause ();
			if (isFacingRight) {
				playerSprite.sprite = SpriteList [5];
				transform.rotation = Quaternion.Euler (0, 0, 45f);
			} else {
				playerSprite.sprite = SpriteList [4];
					transform.rotation = Quaternion.Euler (0, 0, -45f);
			}
		}
	}
}