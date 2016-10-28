using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Riccle : Enemy {
	private bool isFacingRight = true; // 向いている方向判定
	private bool isMovingUp = false; // 上下どちらに移動するか判定
	private float moveUpSpeed = 0.1f; // 上昇速度
	private float moveDownSpeed = 0.3f; // 落下速度
	private bool isAbleToMove = true; // 移動可能か否か（衝突時に使用）
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 1; // 衝突時の待機時間
	private GameObject playerMover; // プレイヤー情報取得用
	private CircleCollider2D getCollider; // Collider取得用

	// 縦方向当たり判定
	private bool IsVerticalCollied(){
		bool isVerCol;
		if(isMovingUp) {
			isVerCol = Physics2D.Linecast(transform.position, transform.position + transform.up * (getCollider.bounds.size.y * 0.5f), groundLayer);
		} else {
			isVerCol = Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.bounds.size.y * 0.5f), groundLayer);
		}
		return isVerCol;
	}

	// プレイヤーのいる方向取得
	private bool IsPlayerRightside( ) {
		if(playerMover.transform.position.x > transform.position.x) {
			return true;
		}
		return false;
	}

	//方向変換
	public void ChangeFace(){
		isFacingRight = !isFacingRight;
	}

	//プロパティ--------------------------------
	public float MoveUpSpeed{
		set{moveUpSpeed = value;}
		get{return moveUpSpeed;}
	}

	public float MoveDownSpeed{
		set{moveDownSpeed = value;}
		get{return moveDownSpeed;}
	}
	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		enemySprite.sprite = SpriteList[0];
		playerMover = GameObject.Find("gamePlayer");
		// Collider取得、サイズの取得
		getCollider = GetComponent<CircleCollider2D>( );
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			// 当たり判定ON
			if(!getCollider.enabled)
				getCollider.enabled = true;
			
			// 上昇処理
			if(isMovingUp) {
				if(isAbleToMove) {
					// 移動
					transform.Translate(Vector2.up * moveUpSpeed);

					// 天井到達で2秒待機、落下モードに切り替え
					if(IsVerticalCollied( )) {
						// めり込み対策
						while(IsVerticalCollied( ) ) {
							transform.Translate(Vector3.down * 0.02f);
						}

						isMovingUp = false;
						isAbleToMove = false;
						waitTime = 2;
					}
				}
				else {
					// 待機時間進行。0で行動開始
					waitTime -= Time.deltaTime;
					if(waitTime <= 0) {
						isAbleToMove = true;
					}
				}
			}
			// 落下処理
			else {
				if(isAbleToMove) {
					// 移動
					transform.Translate(Vector2.down * moveDownSpeed);

					// 地面衝突で1秒待機、昇降モードに切り替え
					if(IsVerticalCollied( )) {
						// 地面めり込み対策
						while(IsVerticalCollied( ) ) {
							transform.Translate(Vector3.up * 0.02f);
						}

						isMovingUp = true;
						isAbleToMove = false;
						waitTime = 1;
					}
				}
				else {
					// 待機時間進行。0で行動開始
					waitTime -= Time.deltaTime;
					if(waitTime <= 0) {
						isAbleToMove = true;
					}
				}
			}
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
