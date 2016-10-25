using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_right : Enemy {
	private bool isFacingRight = true; // 向いている方向判定
	private bool isMovingUp = true; // 上下どちらに移動するか判定
	private bool isMovingVertical = true; // 縦移動/横移動の判定
	private float moveSpeed = 0.05f; // 移動速度
	private bool isAbleToAttack = false; // 攻撃可能か否か
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 0; // 衝突時の待機時間
	private bool changeFlag = false; // 衝突時の方向転換判定
	private GameObject playerMover; // プレイヤー情報取得用
	private Vector3 oldPosition; // 前回位置保存用
	private BoxCollider2D getCollider; // Collider取得用
	private Vector3 startPos; // 初期位置保存用
	private int attackPhase = 0; // 攻撃のフェーズ
	private int atkCnt = 0; // 攻撃回数カウント

	// 縦方向当たり判定
	private bool IsVerticalCollied(){
		bool isVerCol;
		if(isMovingUp) {
			isVerCol = Physics2D.Linecast(transform.position, transform.position + transform.up * (getCollider.size.y * 0.5f + getCollider.offset.y), groundLayer);
		} else {
			isVerCol = Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.size.y * 0.5f - getCollider.offset.y), groundLayer);
		}
		return isVerCol;
	}

	// 横方向当たり判定
	private bool IsHorizontalCollied(){
		if (isFacingRight) {
			// 障害物と画面端の両方で当たり判定
			if(Physics2D.Linecast(transform.position, transform.position + transform.right * (getCollider.size.x * 0.5f + getCollider.offset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position + transform.right * (getCollider.size.x * 0.5f + getCollider.offset.x), groundLayer) )
				return true;
		}
		else {
			if(Physics2D.Linecast(transform.position, transform.position - transform.right * (getCollider.size.x * 0.5f - getCollider.offset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position - transform.right * (getCollider.size.x * 0.5f - getCollider.offset.x), groundLayer) )
				return true;
		}
		return false;
	}

	// プレイヤーとの軸比較
	private bool IsSameAxis( ) {
		if(isMovingUp) {
			if(playerMover.transform.position.y < transform.position.y)
				return true;
		}
		else {
			if(playerMover.transform.position.y > transform.position.y)
				return true;
		}
		return false;
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

	//方向変換
	public void ChangeFace(){
		isFacingRight = !isFacingRight;
	}

	//プロパティ--------------------------------
	public float MoveSpeed{
		private set{moveSpeed = value;}
		get{return moveSpeed;}
	}
	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		playerMover = GameObject.Find("gamePlayer");
		// Colliderのサイズ取得
		getCollider =  GetComponent<BoxCollider2D>( );
		// 初期位置保存 手の落下地点は　初期値　x = 0（中央）　-初期値　の３点になる
		startPos = transform.position;
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate ( ) {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			switch(attackPhase) {
			case 0: {
					waitTime -= Time.deltaTime;
					if(waitTime < 0.3f) {
						enemySprite.sprite = SpriteList[1];
					}
					else if(waitTime < 0) {
						isAbleToAttack = true;
						attackPhase = 1;
					}
					break;
				}
			case 1: {
					if(isAbleToAttack) {
						transform.Translate(Vector3.down * moveSpeed);

						if(IsVerticalCollied( ) ) {
							isAbleToAttack = false;
							waitTime = 1.0f;
						}
					}
					else {
						waitTime -= Time.deltaTime;
						if(waitTime <= 0.5f) {
							enemySprite.sprite = SpriteList[0];
						}
						else if(waitTime < 0) {
							isAbleToAttack = true;
						}
					}

					// とりあえずメモ
					// 通常時
					getCollider.size = new Vector3(5.9f, 2.8f, 0.0f);

					// 攻撃時
					getCollider.size = new Vector3(7.3f, 2.4f, 0.0f);

					break;
				}
			case 2: {
					
					break;
				}
			}


/*
			// 縦移動時処理
			if(isMovingVertical) {
				// プレイヤーと軸が合うか障害物に衝突したら、上下移動をやめて横移動を開始する。
				// もしくは、移動処理を行っているのに前フレームから変化がなければ、移動を中断
				if(IsSameAxis( ) || IsVerticalCollied( ) || !isAbleToMove || oldPosition == transform.position) {
					isMovingVertical = false;
					moveSpeed = 0.15f;
					isAbleToMove = true;
				}
				else {
					// 前回位置保存
					oldPosition = transform.position;

					// 移動処理
					if(isMovingUp) {
						transform.Translate(Vector2.up * moveSpeed);
					}
					else {
						transform.Translate(Vector2.down * moveSpeed);
					}
				}
			}
			// 横移動処理
			else {
				// 障害物か画面端に衝突したらしばらく動きを止める
				// もしくは、移動処理を行っているのに前フレームから変化がなければ、移動を中断
				if((IsHorizontalCollied( ) || oldPosition == transform.position) && isAbleToMove) {
					ChangeFace( );
					waitTime = 1.0f;
					changeFlag = true;
					isAbleToMove = false;
					oldPosition.x += 1; // 衝突後の待機状態が終わった時点でoldPosition == positionを満たしてしまうため、数値をずらしておく
				}

				//もし移動ができるならば
				if(isAbleToMove) {
					// 前回位置保存
					oldPosition = transform.position;

					//左右移動
					if(isFacingRight) {
						transform.Translate(Vector2.right * moveSpeed);
					}
					else {
						transform.Translate(Vector2.left * moveSpeed);
					}
				}
				// 衝突から0.5秒で振り返る
				else if(waitTime <= 0.5f && changeFlag) {
					changeFlag = false;
					waitTime -= Time.deltaTime;
					if(isFacingRight) {
						enemySprite.sprite = SpriteList[0];
					}
					else {
						enemySprite.sprite = SpriteList[1];
					}
				}
				// 衝突から1秒で縦移動再開
				else if(waitTime <= 0) {
					isAbleToMove = true;
					isMovingVertical = true;
					ChkMovingWay( );
					moveSpeed = 0.025f;
				}
				else {
					// 衝突後の行動不可時間進行
					waitTime -= Time.deltaTime;
				}
			}*/
		}
	}
}
