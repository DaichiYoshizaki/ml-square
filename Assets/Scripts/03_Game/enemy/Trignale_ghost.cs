using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_ghost : MonoBehaviour {
	private bool isFacingRight = true; // 向いている方向判定
	private bool isMovingUp = true; // 上下どちらに移動するか判定
	private Vector2 moveSpeed; // 移動速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	private GameObject playerMover; // プレイヤー情報取得用
	static bool enemyPauseFlag = false; // ポーズ状態フラグ

	// プレイヤーのいる方向取得
	private void IsPlayerRightside( ) {
		if(playerMover.transform.position.x > transform.position.x) {
			isFacingRight = true;
		}
		else {
			isFacingRight = false;
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

	// ポーズ状態のON/OFF
	static public void EnemyPauseChange( ) {
		enemyPauseFlag = !enemyPauseFlag;
	}

	//プロパティ--------------------------------


	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		enemySprite.sprite = SpriteList[0];
		playerMover = GameObject.Find("gamePlayer");
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			transform.Translate(moveSpeed);

			ChkMovingWay( );
			IsPlayerRightside( );

			if(isMovingUp) {
				moveSpeed.y += 0.0005f;
				if(moveSpeed.y >= 0.04f)
					moveSpeed.y = 0.04f;
			}
			else {
				moveSpeed.y -= 0.0005f;
				if(moveSpeed.y <= -0.04f)
					moveSpeed.y = -0.04f;
			}

			if(isFacingRight) {
				moveSpeed.x += 0.0005f;
				if(moveSpeed.x >= 0.04f)
					moveSpeed.x = 0.04f;
			}
			else {
				moveSpeed.x -= 0.0005f;
				if(moveSpeed.x <= -0.04f)
					moveSpeed.x = -0.04f;
			}
		}
	}
}
