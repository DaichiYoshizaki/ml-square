﻿/*******************************************************************************************************************************************************
 * アカゴナルクラス
 * 
 * 上下に突進を繰り返す
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_red : Enemy {
	private bool isMovingUp = true; // 上下どちらに移動するか判定
	private float moveSpeed = 0.15f; // 移動速度
	private bool isAbleToMove = true; // 移動可能か否か（衝突時に使用）
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 0; // 衝突時の待機時間
	private GameObject playerMover; // プレイヤー情報取得用
	private Vector3 oldPosition; // 前回位置保存用
	private BoxCollider2D getCollider; // Collider取得用

	// 縦方向当たり判定
	private bool IsVerticalCollied( ) {
		bool isVerCol;
		if(isMovingUp) {
			isVerCol = Physics2D.Linecast(transform.position, transform.position + transform.up * (getCollider.bounds.size.y * 0.5f + getCollider.offset.y), groundLayer);
		} else {
			isVerCol = Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.bounds.size.y * 0.5f - getCollider.offset.y), groundLayer);
		}
		return isVerCol;
	}

	// プレイヤーのいる方向取得
	private void IsPlayerRightside( ) {
		if(playerMover.transform.position.x > transform.position.x) {
			enemySprite.sprite = SpriteList[0];
		}
		else {
			enemySprite.sprite = SpriteList[1];
		}
	}

	//方向変換
	public void ChangeUpDown( ) {
		isMovingUp = !isMovingUp;
	}


	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>( );
		// プレイヤー情報取得
		playerMover = GameObject.Find("gamePlayer");
		// Collider取得
		getCollider =  GetComponent<BoxCollider2D>( );
		// プレイヤーの位置から初期の向きを設定
		IsPlayerRightside( );
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

			// 障害物か画面端に衝突したらしばらく動きを止める
			// もしくは、移動処理を行っているのに前フレームから変化がなければ、移動を中断
			if( (IsVerticalCollied( ) || oldPosition == transform.position) && isAbleToMove) {
				waitTime = 2.0f; // 待機時間設定
				ChangeUpDown( ); // 移動方向反転
				isAbleToMove = false; // 待機状態に移行
				oldPosition.x += 1; // 衝突後の待機状態が終わった時点でoldPosition == positionを満たしてしまうため、数値をずらしておく
				SoundManager.Instance.PlaySE(3); // SE再生
			}

			//もし移動ができるならば
			if(isAbleToMove) {
				// 前回位置保存
				oldPosition = transform.position;

				// 上下移動
				if(isMovingUp) {
					transform.Translate(Vector2.up * moveSpeed * Time.deltaTime * timeAdjust);
				}
				else {
					transform.Translate(Vector2.down * moveSpeed * Time.deltaTime * timeAdjust);
				}
			}
			// 衝突から1秒で縦移動再開
			else if(waitTime <= 0) {
				isAbleToMove = true;
				// プレイヤーの方向に振り向く
				IsPlayerRightside( );
			}
			else {
				// 衝突後の行動不可時間進行
				waitTime -= Time.deltaTime;
			}
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
