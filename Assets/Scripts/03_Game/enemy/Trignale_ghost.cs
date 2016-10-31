/*******************************************************************************************************************************************************
 * トリゴーストクラス
 * 
 * ゆっくりとプレイヤーを追尾して追いかけ続ける。壁や床も貫通してゆく
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_ghost : Enemy {
	private bool isFacingRight = true; // 向いている方向判定
	private bool isMovingUp = true; // 上下どちらに移動するか判定
	private Vector2 moveSpeed; // 移動速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	private GameObject playerMover; // プレイヤー情報取得用
	private BoxCollider2D getCollider; // Collider取得用

	// プレイヤーのいる方向取得
	private void IsPlayerRightside( ) {
		if(playerMover.transform.position.x > transform.position.x) {
			isFacingRight = true;
			enemySprite.sprite = SpriteList[0];
		}
		else {
			isFacingRight = false;
			enemySprite.sprite = SpriteList[1];
		}
	}

	// プレイヤーの位置から上下どちらに移動するかを判定
	private void ChkMovingWay( ) {
		if(playerMover.transform.position.y < transform.position.y) {
			isMovingUp = false;
		}
		else {
			isMovingUp = true;
		}
	}


	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>( );
		enemySprite.sprite = SpriteList[0];
		// プレイヤー情報取得
		playerMover = GameObject.Find("gamePlayer");
		// Collider取得
		getCollider =  GetComponent<BoxCollider2D>( );
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

			// 移動
			transform.Translate(moveSpeed * Time.deltaTime * timeAdjust);

			// 上下左右どの方向にプレイヤーいるか確認。他のやつの使い回しなので関数名は許して
			ChkMovingWay( );
			IsPlayerRightside( );

			// 上下移動
			if(isMovingUp) {
				moveSpeed.y += 0.0005f * Time.deltaTime * timeAdjust;
				if(moveSpeed.y >= 0.04f)
					moveSpeed.y = 0.04f;
			}
			else {
				moveSpeed.y -= 0.0005f * Time.deltaTime * timeAdjust;
				if(moveSpeed.y <= -0.04f)
					moveSpeed.y = -0.04f;
			}

			// 左右移動
			if(isFacingRight) {
				moveSpeed.x += 0.0005f * Time.deltaTime * timeAdjust;
				if(moveSpeed.x >= 0.04f)
					moveSpeed.x = 0.04f;
			}
			else {
				moveSpeed.x -= 0.0005f * Time.deltaTime * timeAdjust;
				if(moveSpeed.x <= -0.04f)
					moveSpeed.x = -0.04f;
			}
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
