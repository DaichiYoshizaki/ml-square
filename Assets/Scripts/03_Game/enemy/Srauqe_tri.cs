﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Srauqe_tri : Enemy {
	private bool isFacingRight; // 向いている方向判定
	private bool isAbleToJump = false; // ジャンプ可否判定
	private float gravity = 0.01f; // 重力加速度
	private float jumpSpeed = -0.1f; // ジャンプ速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 1; // 衝突時の待機時間
	private GameObject playerMover; // プレイヤー情報取得用
	private Vector3 colSize; // Colliderのサイズ取得用
	private Vector2 colOffset; // Colliderのoffset取得用

	// 縦方向当たり判定
	private bool IsVerticalCollied(){
		bool isVerCol;
		if(jumpSpeed > 0) {
			isVerCol = Physics2D.Linecast(transform.position, transform.position + transform.up * (colSize.y * 0.5f + colOffset.y), groundLayer);
		} else {
			isVerCol = Physics2D.Linecast(transform.position, transform.position - transform.up * (colSize.y * 0.5f - colOffset.y), groundLayer);
		}
		return isVerCol;
	}

	// ジャンプ
	public void Jump(float jumpPower){
		if (isAbleToJump) {
			jumpSpeed = jumpPower;
			isAbleToJump = false;
			// SE再生
			SoundManager.Instance.PlaySE(1);
		}
	}

	// プレイヤーが一定距離以内にいるならジャンプする
	private bool ChkDistance( ) {
		if(Mathf.Abs(playerMover.transform.position.x - transform.position.x) < 2.2f) {
			return true;
		}
		return false;
	}

	//プロパティ--------------------------------


	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		enemySprite.sprite = SpriteList[0];
		playerMover = GameObject.Find("gamePlayer");
		// Colliderのサイズ取得
		colSize =  GetComponent<BoxCollider2D>( ).bounds.size;
		colOffset = GetComponent<BoxCollider2D>( ).offset;
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			if(!isAbleToJump) {
				transform.Translate(Vector2.up * jumpSpeed);
				jumpSpeed -= gravity;
				// 落下速度制限　めり込み処理が重くなりすぎないように
				if(jumpSpeed < -1)
					jumpSpeed = -1;

				// 着地したら2秒待機
				if(IsVerticalCollied( ) ) {
					if(jumpSpeed < 0) {
						isAbleToJump = true;
						jumpSpeed = 0;
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
				if(waitTime <= 0 && ChkDistance( ) ) {
					// ジャンプ
					Jump(0.3f);
					isAbleToJump = false;
				}
			}
		}
	}
}
