﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Srauqe : MonoBehaviour {
	private bool isAbleToJump = false; // ジャンプ可否判定
	private float gravity = 0.01f; // 重力加速度
	private float jumpSpeed = -0.1f; // ジャンプ速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 1; // 衝突時の待機時間
	static bool enemyPauseFlag = false; // ポーズ状態フラグ
	private Vector3 colSize; // Colliderのサイズ取得用
	private Vector2 colOffset; // Colliderのoffset取得用

	// 着地判定
	private bool IsGrounded(){
		return Physics2D.Linecast(transform.position, transform.position - transform.up * (colSize.y * 0.5f - colOffset.y + 0.05f), groundLayer);
	}

	// ジャンプ
	public void Jump(float jumpPower){
		if (isAbleToJump) {
			jumpSpeed = jumpPower;
			isAbleToJump = false;
		}
	}

	// ポーズ状態のON/OFF
	public void EnemyPauseChange( ) {
		enemyPauseFlag = !enemyPauseFlag;
	}

	//プロパティ--------------------------------


	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		enemySprite.sprite = SpriteList[0];
		colSize =  GetComponent<BoxCollider2D>( ).bounds.size;
		colOffset = GetComponent<BoxCollider2D>( ).offset;
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			// 着地したら2秒待機
			if(!isAbleToJump) {
				transform.Translate(Vector2.up * jumpSpeed);
				jumpSpeed -= gravity;

				if(IsGrounded( )) {
					isAbleToJump = true;
					jumpSpeed = 0;
					waitTime = 2;
				}
			}
			else {
				waitTime -= Time.deltaTime;
				if(waitTime <= 0) {
					// ジャンプ
					Jump(0.3f);
					isAbleToJump = false;
				}
			}
		}
	}
}