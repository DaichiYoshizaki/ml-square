/*******************************************************************************************************************************************************
 * ライゴナルクラス
 * 
 * 左右に一定距離をゆっくり巡回する
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_lime : Enemy {
	private bool isFacingRight = true; // 向いている方向判定
	private float moveSpeed = 0.05f; // 移動速度
	private float moveDistance = 0; // 移動距離
	private bool isAbleToMove = true; // 移動可能か否か（衝突時に使用）
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private GameObject playerMover; // プレイヤー情報取得用
	private Vector3 oldPosition; // 前回位置保存用
	private BoxCollider2D getCollider; // Collider取得用

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

	//方向変換
	public void ChangeFace( ) {
		isFacingRight = !isFacingRight;
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
			if((IsHorizontalCollied( ) || oldPosition == transform.position || moveDistance >= 3.0f) && isAbleToMove) {
				moveDistance = 0;
				// 振り向き処理
				ChangeFace( );
				if(isFacingRight) {
					enemySprite.sprite = SpriteList[0];
				}
				else {
					enemySprite.sprite = SpriteList[1];
				}
				oldPosition.x += 1; // 衝突後の待機状態が終わった時点でoldPosition == positionを満たしてしまうため、数値をずらしておく
			}

			// 前回位置保存
			oldPosition = transform.position;

			//左右移動
			if(isFacingRight) {
				transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * timeAdjust);
			}
			else {
				transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * timeAdjust);
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
