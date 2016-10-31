/*******************************************************************************************************************************************************
 * イェアーククラス
 * 
 * プレイヤーに向かってジャンプ移動し続ける
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Srauqe_yellow : Enemy {
	private bool isFacingRight; // 向いている方向判定
	private bool isAbleToJump = false; // ジャンプ可否判定
	private float gravity = 0.01f; // 重力加速度
	private float jumpSpeed = -0.1f; // ジャンプ速度
	private float moveSpeed = 0; // 移動速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 1; // 衝突時の待機時間
	private GameObject playerMover; // プレイヤー情報取得用
	private BoxCollider2D getCollider; // Collider取得用

	// 縦方向当たり判定
	private bool IsVerticalCollied(){
		bool isVerCol;
		if(jumpSpeed > 0) {
			isVerCol = Physics2D.Linecast(transform.position, transform.position + transform.up * (getCollider.bounds.size.y * 0.5f + getCollider.offset.y), groundLayer);
		} else {
			isVerCol = Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.bounds.size.y * 0.5f - getCollider.offset.y), groundLayer);
		}
		return isVerCol;
	}

	// 横方向当たり判定
	private bool IsHorizontalCollied( ) {
		if (isFacingRight) {
			// 障害物と画面端の両方で当たり判定
			if(Physics2D.Linecast(transform.position, transform.position + transform.right * (getCollider.bounds.size.x * 0.5f + getCollider.offset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position + transform.right * (getCollider.bounds.size.x * 0.5f + getCollider.offset.x), groundLayer) )
				return true;
		}
		else {
			if(Physics2D.Linecast(transform.position, transform.position - transform.right * (getCollider.bounds.size.x * 0.5f - getCollider.offset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position - transform.right * (getCollider.bounds.size.x * 0.5f - getCollider.offset.x), groundLayer) )
				return true;
		}
		return false;
	}

	// ジャンプ
	public void Jump(float jumpPower) {
		if (isAbleToJump) {
			jumpSpeed = jumpPower;
			isAbleToJump = false;
			// SE再生
			SoundManager.Instance.PlaySE(1);
		}
	}

	// プレイヤーの位置から左右どちらに移動するかを判定
	private void ChkMovingWay( ) {
		if(playerMover.transform.position.x < transform.position.x) {
			isFacingRight = false;
		}
		else {
			isFacingRight = true;
		}
	}
		

	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>( );
		enemySprite.sprite = SpriteList[0];
		playerMover = GameObject.Find("gamePlayer");
		// Collider取得
		getCollider =  GetComponent<BoxCollider2D>( );
		ChkMovingWay( );
	}

	void Update( ) {
	}

	// Update is called once per frame
	void FixedUpdate( ) {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			// 当たり判定ON
			if(!getCollider.enabled)
				getCollider.enabled = true;

			if(!isAbleToJump) {
				transform.Translate(new Vector3(moveSpeed, jumpSpeed, 0) * Time.deltaTime * timeAdjust);

				// 移動量から重力加速度分を引く
				jumpSpeed -= gravity * Time.deltaTime * timeAdjust;
				// 落下速度制限　めり込み処理が重くなりすぎないように
				if(jumpSpeed < -1)
					jumpSpeed = -1;

				// 横方向に壁やフィールドにぶつかったら、横方向の移動量を0に
				if(IsHorizontalCollied( ) ) {
					// めり込み対策
					while(IsHorizontalCollied( ) ) {
						transform.Translate(new Vector3(moveSpeed, 0.0f, 0.0f) * -0.2f);
					}
					moveSpeed = 0;
				}
				// 着地したら2秒待機
				if(IsVerticalCollied( ) ) {
					if(jumpSpeed < 0) {
						isAbleToJump = true;
						jumpSpeed = 0;
						moveSpeed = 0;
						waitTime = 2;
						// 地面めり込み対策
						while(IsVerticalCollied( ) ) {
							transform.Translate(Vector3.up * 0.02f);
						}
					}
					// 上にぶつかったら縦方向の移動量を0にして後は自然落下させる
					else {
						jumpSpeed = 0;
					}
				}
			}
			else {
				waitTime -= Time.deltaTime;
				if(waitTime <= 0) {
					// ジャンプ
					Jump(0.3f);
					// 左右どちらにプレイヤーがいるか確認
					ChkMovingWay( );
					if(isFacingRight) {
						moveSpeed = 0.1f;
					}
					else {
						moveSpeed = -0.1f;
					}
					isAbleToJump = false;
				}
			}
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
