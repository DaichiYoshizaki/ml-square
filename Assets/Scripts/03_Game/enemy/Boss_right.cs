using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_right : Enemy {
	private Vector3 moveSpeed; // 移動速度
	private Vector3 attackSpeed; // 攻撃速度
	private bool isAbleToAttack; // 攻撃可能か否か
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime; // 待機時間
	private bool changeFlag; // 画像切り替え判定
	private bool moveFlag; // 移動開始判定
	private BoxCollider2D getCollider; // Collider取得用
	private Vector3 startPos; // 初期位置保存用
	private int attackPhase; // 攻撃のフェーズ
	private int atkCnt; // 攻撃回数カウント
	private Vector3 neutralCollider; // 待機時当たり判定
	private Vector3 attackCollider; // 攻撃時当たり判定
	private Vector3 nextDistination; // 次の移動先座標
	private float elapseTime; // 経過時間保存用
	private Color setColor; // アルファ値変更用
	private GameObject getLeft; // 左手取得
	private float speedGear; // ギヤ。早いほどボスの攻撃が早くなる。
	private bool endFlag; // 終了フラグ

	// 縦方向当たり判定
	private bool IsVerticalCollied( ) {
		return Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.size.y * 0.5f - getCollider.offset.y), groundLayer);
	}

	public void NextPhase(int nextPhase) {
		attackPhase = nextPhase;
	}

	//プロパティ--------------------------------

	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();

		// 画像を透明に
		setColor = enemySprite.color;
		setColor.a = 0;
		enemySprite.color = setColor;
		// Collider取得
		getCollider =  GetComponent<BoxCollider2D>( );
		getCollider.enabled = false;
		// 待機時と攻撃時それぞれのCollierサイズ。画像変えたら数値変更してください
		neutralCollider = new Vector3(5.9f, 2.8f, 0.0f);
		attackCollider = new Vector3(7.3f, 2.4f, 0.0f);
		// 初期位置保存 手の落下地点は　1.初期値　2.x = 0（中央）　3.-初期値　の３箇所になる
		startPos = transform.position;
		// 左手取得
		getLeft = GameObject.Find("enemy_boss_left");

		// 変数設定
		isAbleToAttack = false;
		changeFlag = true;
		moveFlag = false;
		endFlag = false;

		attackPhase = 0;
		atkCnt = 0;

		waitTime = 2.0f;
		elapseTime = 0;

		attackSpeed = new Vector3(0.0f, -0.3f, 0.0f);
		speedGear = 1.0f;
	}

	void Update( ) {
	}

	// Update is called once per frame
	void FixedUpdate ( ) {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			switch(attackPhase) {
			case 0: {
					waitTime -= Time.deltaTime * speedGear;

					// 透明解除
					if(setColor.a != 1) {
						setColor.a += Time.deltaTime * 0.7f * speedGear;
						if(setColor.a > 1) {
							setColor.a = 1;
							getCollider.enabled = true;
						}
						enemySprite.color = setColor;
					}

					if(changeFlag && waitTime <= 0.5f) {
						// 画像を攻撃用に切り替え、それに伴い当たり判定も更新
						enemySprite.sprite = SpriteList[1];
						getCollider.size = attackCollider;
						changeFlag = false;
					}
					else if(waitTime < 0) {
						isAbleToAttack = true;
						attackPhase = 1;
					}

					break;
				}
			case 1: {
					waitTime -= Time.deltaTime * speedGear;
					if(isAbleToAttack) {
						if(changeFlag && waitTime <= 0.5f) {
							// 画像を攻撃用に切り替え、それに伴い当たり判定も更新
							enemySprite.sprite = SpriteList[1];
							getCollider.size = attackCollider;
							changeFlag = false;
						}
						else if(waitTime < 0) {
							transform.Translate(attackSpeed * speedGear);

							if(IsVerticalCollied( ) ) {
								isAbleToAttack = false;
								waitTime = 1.0f;
								changeFlag = true;
							}
						}
					}
					else if(!moveFlag) {
						if(changeFlag && waitTime <= 0.5f) {
							// 画像を待機用に切り替え、それに伴い当たり判定も更新
							enemySprite.sprite = SpriteList[0];
							getCollider.size = neutralCollider;
							changeFlag = false;
						}
						else if(waitTime < 0) {
							moveFlag = true;
							atkCnt++;
							// 3回攻撃したら次のフェーズへ
							if(atkCnt >= 3) {
								attackPhase = 2;
								moveFlag = false;
								changeFlag = true;
							}
							// 次の落下開始地点登録、そこまでの移動距離を求める
							nextDistination = new Vector3(startPos.x - startPos.x * atkCnt, startPos.y, 0.0f);
							moveSpeed = new Vector3(nextDistination.x - transform.position.x, nextDistination.y - transform.position.y, 0.0f);
						}
					}
					else {
						// moveSpeedが移動速度でなく総移動距離になっているので、deltaTimeをかける
						transform.Translate(moveSpeed * Time.deltaTime);
						elapseTime += Time.deltaTime;
						// 1秒かけて移動をほぼ完了したら目的地に強制移動（行き過ぎ防止策）して、各種設定
						if(elapseTime > 1.0f) {
							transform.position = nextDistination;
							isAbleToAttack = true;
							moveFlag = false;
							changeFlag = true;
							waitTime = 1.0f;
							elapseTime = 0;
						}
					}

					break;
				}
			case 2: {
					// 攻撃後透過して消える処理
					if(setColor.a != 0) {
						setColor.a -= Time.deltaTime * 0.7f;
						// 透過が割ったら判定を消す
						if(setColor.a < 0) {
							setColor.a = 0;
							getCollider.enabled = false;
							// 初期位置に戻す
							transform.position = startPos;

							if(speedGear < 1.1f) {
								if(getLeft != null) {
									getLeft.GetComponent<Boss_left>( ).NextPhase(1);
								}
								else {
									attackPhase = 3;
								}
								if(endFlag)
									enemyPauseFlag = true;
							}
							else {
								if(getLeft != null) {
									getLeft.GetComponent<Boss_left>( ).NextPhase(3);
								}
								else {
									attackPhase = 4;
								}
							}

							// 終了フラグがONならばポーズ
							if(endFlag)
								enemyPauseFlag = true;
						}
						enemySprite.color = setColor;
					}

					break;
				}
			case 3: {
					speedGear = 2.0f;
					waitTime = 2.0f;
					attackPhase = 0;
					atkCnt = 0;

					break;
				}
			case 4: {
					speedGear = 1.0f;
					waitTime = 2.0f;
					attackPhase = 0;
					atkCnt = 0;
					getLeft.GetComponent<Boss_left>( ).NextPhase(4);
					endFlag = true;

					break;
				}
			case 99: {
					// 未完成時用の無限ループ（第１フェーズに戻る）
					attackPhase = 1;
					atkCnt = 0;
					nextDistination = startPos;
					moveSpeed = new Vector3(nextDistination.x - transform.position.x, nextDistination.y - transform.position.y, 0.0f);
					isAbleToAttack = false;
					moveFlag = true;

					break;
				}
			default:
				break;
			}
		}
	}
}
