/*******************************************************************************************************************************************************
 * ピンゴナルクラス
 * 
 * 上下に一定距離をゆっくり巡回する（こいつとしては寝てるだけ）
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_pink : Enemy {
	private bool isMovingUp = true; // 上下どちらに移動するか判定
	private float moveSpeed = 0.05f; // 移動速度
	private float moveDistance = 0; // 移動距離
	private bool isAbleToMove = true; // 移動可能か否か（衝突時に使用）
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
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

	//方向変換
	public void ChangeUpDown( ) {
		isMovingUp = !isMovingUp;
	}


	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>( );
		enemySprite.sprite = SpriteList[0];
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

			// 障害物か画面端に衝突したらしばらく動きを止める
			// もしくは、移動処理を行っているのに前フレームから変化がなければ、移動を中断
			if( (IsVerticalCollied( ) || oldPosition == transform.position || moveDistance >= 3.0f) && isAbleToMove) {
				moveDistance = 0;
				ChangeUpDown( );
				oldPosition.x += 1; // 衝突後の待機状態が終わった時点でoldPosition == positionを満たしてしまうため、数値をずらしておく
			}

			// 前回位置保存
			oldPosition = transform.position;

			// 上下移動
			if(isMovingUp) {
				transform.Translate(Vector2.up * moveSpeed * Time.deltaTime * timeAdjust);
			}
			else {
				transform.Translate(Vector2.down * moveSpeed * Time.deltaTime * timeAdjust);
			}
			// 移動距離加算
			moveDistance += moveSpeed * Time.deltaTime * timeAdjust;
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
