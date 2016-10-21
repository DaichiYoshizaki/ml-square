using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Srauqe : MonoBehaviour {
	private bool isAbleToJump = true; // ジャンプ可否判定
	private Rigidbody2D rb2d; // 移動量加算用
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 1; // 衝突時の待機時間


	// 着地判定
	private bool IsGrounded(){
		return Physics2D.Linecast(transform.position, transform.position - transform.up * 0.7f, groundLayer);
	}

	// ジャンプ
	public void Jump(float jumpPower){
		if (isAbleToJump) {
			rb2d.AddForce (Vector2.up * jumpPower);
			isAbleToJump = false;
		}
	}

	//プロパティ--------------------------------


	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// 着地したら2秒待機
		if(IsGrounded( ) && !isAbleToJump) {
			isAbleToJump = true;
			waitTime = 2;
		}

		if(isAbleToJump) {
			waitTime -= Time.deltaTime;
			if(waitTime <= 0) {
				// ジャンプ
				Jump(2500);
				isAbleToJump = false;
			}
		}
	}
}
