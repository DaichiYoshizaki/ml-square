using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_left : Enemy {
	private Vector3 moveSpeed; // 移動速度
	private Vector3 attackSpeed; // 攻撃速度
	private bool isAbleToAttack; // 攻撃可能か否か
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime; // 待機時間
	private bool changeFlag; // 画像切り替え判定
//	private bool moveFlag; // 移動開始判定
	private BoxCollider2D getCollider; // Collider取得用
	private Vector3 startPos; // 初期位置保存用
	private int attackPhase; // 攻撃のフェーズ
	private int atkCnt; // 攻撃回数カウント
	private Vector3 neutralCollider; // 待機時当たり判定
	private Vector3 attackCollider; // 攻撃時当たり判定
	private Vector3 nextDistination; // 次の移動先座標
//	private float elapseTime; // 経過時間保存用
	private Color setColor; // アルファ値変更用
	private GameObject getRight; // 右手取得
	private float speedGear; // ギヤ。早いほどボスの攻撃が早くなる。
	private bool endFlag; // 終了フラグ

	// 縦方向当たり判定
	private bool IsVerticalCollied(){
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
		// 初期位置保存。突撃する高さは 初期位置 と 初期位置 + 幅 * 0.8 の2箇所になる
		startPos = transform.position;
		// 回転。左右どちらからスタートするかによって回転角度を変える
		if(startPos.x > 0)
			transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f) );
		else
			transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f) );
		// 右手取得
		getRight = GameObject.Find("enemy_boss_right");

		// 変数設定
		isAbleToAttack = false;
		changeFlag = true;
//		moveFlag = false;
		endFlag = false;

		attackPhase = 0;
		atkCnt = 0;

		waitTime = 2.0f;
//		elapseTime = 0;

		attackSpeed = new Vector3(0.0f, -0.2f, 0.0f);
		speedGear = 1.0f;
	}

	void Update( ) {
	}

	// Update is called once per frame
	void FixedUpdate( ) {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			switch(attackPhase) {
			case 0: {
					// 右手に呼ばれるまで何もしない
					break;
				}
			case 1: {
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
						attackPhase = 2;
					}

					break;
				}
			case 2: {
					if(isAbleToAttack) {
						waitTime -= Time.deltaTime * speedGear;
						if(changeFlag && waitTime <= 0.5f) {
							// 画像を攻撃用に切り替え、それに伴い当たり判定も更新
							enemySprite.sprite = SpriteList[1];
							getCollider.size = attackCollider;
							changeFlag = false;
						}
						else if(waitTime < 0) {
							transform.Translate(attackSpeed * speedGear * Time.deltaTime * 50);

							// 中央〜初期位置の3倍の距離を移動したら方向転換
							// ３倍進めば画面外だろ！という意図なので、ちゃんと画面の端に配置してください、画面内で処理が発生するとワープします
							if(Mathf.Abs(transform.position.x - startPos.x) > Mathf.Abs(startPos.x * 3.0f)) {
								transform.position = new Vector3(transform.position.x, startPos.y + getCollider.size.x * 0.8f, 0.0f);
								transform.Rotate(0.0f, 0.0f, 180.0f);
								atkCnt++;
							}
							// 方向転換後、中央〜初期位置の1.5倍の距離まで移動したら処理終了。上と同じく中央に配置すると途中で止まって消えるよ
							else if(atkCnt != 0 && ( (startPos.x > 0 && transform.position.x > startPos.x * 1.5f) || (startPos.x < 0 && transform.position.x < startPos.x * 1.5f) ) ) {
								attackPhase = 0;
								atkCnt = 0;
								waitTime = 2.0f;
								isAbleToAttack = false;
								changeFlag = true;

								// 初期位置に戻す
								transform.position = startPos;
								transform.Rotate(new Vector3(0.0f, 0.0f, -180.0f) );

								// 画像を待機状態に戻して透明に
								enemySprite.sprite = SpriteList[0];
								getCollider.size = neutralCollider;
								getCollider.enabled = false;

								setColor.a = 0;
								enemySprite.color = setColor;

								// 終了フラグがONならばポーズ、でなければ右手へフェーズ変更の命令
								if(endFlag) {
									enemyPauseFlag = true;
								}
								else {
									if(speedGear < 1.1f) {
										if(getRight != null) {
											getRight.GetComponent<Boss_right>( ).NextPhase(3);
										}
										else {
											attackPhase = 3;
										}
									}
									else {
										if(getRight != null) {
											getRight.GetComponent<Boss_right>( ).NextPhase(4);
										}
										else {
											attackPhase = 4;
										}
									}
								}
							}
						}
					}

					break;
				}
			case 3: {
					speedGear = 2.0f;
					isAbleToAttack = true;
					attackPhase = 1;

					break;
				}
			case 4: {
					speedGear = 1.0f;
					isAbleToAttack = true;
					attackPhase = 1;
					endFlag = true;

					break;
				}
			default: {
					break;
				}
			}
		}
	}
}
