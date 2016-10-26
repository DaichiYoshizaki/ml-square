using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_right : Enemy {
	private Vector3 moveSpeed; // 移動速度
	private bool isAbleToAttack = false; // 攻撃可能か否か
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 2; // 待機時間
	private bool changeFlag = true; // 画像切り替え判定
	private bool moveFlag = false; // 移動開始判定
	private BoxCollider2D getCollider; // Collider取得用
	private Vector3 startPos; // 初期位置保存用
	private int attackPhase = 0; // 攻撃のフェーズ
	private int atkCnt = 0; // 攻撃回数カウント
	private Vector3 neutralCollider; // 待機時当たり判定
	private Vector3 attackCollider; // 攻撃時当たり判定
	private Vector3 nextDistination; // 次の移動先座標
	private float elapseTime = 0; // 経過時間保存用

	// 縦方向当たり判定
	private bool IsVerticalCollied( ) {
		return Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.size.y * 0.5f - getCollider.offset.y), groundLayer);
	}

	//プロパティ--------------------------------

	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		// Colliderのサイズ取得
		getCollider =  GetComponent<BoxCollider2D>( );
		// 待機時と攻撃時それぞれのCollierサイズ。画像変えたら数値変更してください
		neutralCollider = new Vector3(5.9f, 2.8f, 0.0f);
		attackCollider = new Vector3(7.3f, 2.4f, 0.0f);
		// 初期位置保存 手の落下地点は　1.初期値　2.x = 0（中央）　3.-初期値　の３箇所になる
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
					if(waitTime < 0.5f && changeFlag) {
						// 画像を攻撃用に切り替え、それに伴い当たり判定も更新
						enemySprite.sprite = SpriteList[1];
						getCollider.size = attackCollider;
						changeFlag = false;
					}
					else if(waitTime < 0) {
						isAbleToAttack = true;
						moveSpeed = new Vector3(0.0f, -0.3f, 0.0f);
						attackPhase = 1;
					}
					break;
				}
			case 1: {
					waitTime -= Time.deltaTime;
					if(isAbleToAttack) {
						if(waitTime <= 0.5f && changeFlag) {
							enemySprite.sprite = SpriteList[1];
							getCollider.size = attackCollider;
							changeFlag = false;
						}
						if(waitTime < 0) {
							transform.Translate(moveSpeed);

							if(IsVerticalCollied( )) {
								isAbleToAttack = false;
								waitTime = 1.0f;
								changeFlag = true;
							}
						}
					}
					else if(!moveFlag) {
						if(waitTime <= 0.5f && changeFlag) {
							// 画像を待機用に切り替え、それに伴い当たり判定も更新
							enemySprite.sprite = SpriteList[0];
							getCollider.size = neutralCollider;
							changeFlag = false;
						}
						else if(waitTime < 0) {
							moveFlag = true;
							atkCnt++;
							if(atkCnt >= 3)
								attackPhase = 2;
							nextDistination = new Vector3(startPos.x - startPos.x * atkCnt, startPos.y, 0.0f);
							moveSpeed = new Vector3(nextDistination.x - transform.position.x, nextDistination.y - transform.position.y, 0.0f);
						}
					}
					else {
						transform.Translate(moveSpeed * Time.deltaTime);
						elapseTime += Time.deltaTime;
						if(elapseTime > 1.0f) {
							transform.position = nextDistination;
							isAbleToAttack = true;
							moveFlag = false;
							changeFlag = true;
							moveSpeed = new Vector3(0.0f, -0.3f, 0.0f);
							waitTime = 1.0f;
							elapseTime = 0;
						}
					}

					break;
				}
			case 2: {
					attackPhase = 1;
					atkCnt = 0;
					nextDistination = startPos;
					moveSpeed = new Vector3(nextDistination.x - transform.position.x, nextDistination.y - transform.position.y, 0.0f);
					isAbleToAttack = false;
					moveFlag = true;

					break;
				}
			}
		}
	}
}
