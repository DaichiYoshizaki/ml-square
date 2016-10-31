/*******************************************************************************************************************************************************
 * ボス左手クラス
 * 
 * 横からの薙ぎ払い攻撃を行う
*******************************************************************************************************************************************************/

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
	private BoxCollider2D getCollider; // Collider取得用
	private Vector3 startPos; // 初期位置保存用
	private int attackPhase; // 攻撃のフェーズ
	private int atkCnt; // 攻撃回数カウント
	private Vector3 neutralCollider; // 待機時当たり判定
	private Vector3 attackCollider; // 攻撃時当たり判定
	private Vector3 nextDistination; // 次の移動先座標
	private Color setColor; // アルファ値変更用
	private GameObject getRight; // 右手取得
	private float speedGear; // ギヤ。早いほどボスの攻撃が早くなる。
	private bool endFlag; // 終了フラグ

	// 縦方向当たり判定
	private bool IsVerticalCollied( ) {
		return Physics2D.Linecast(transform.position, transform.position - transform.up * (getCollider.size.y * 0.5f - getCollider.offset.y), groundLayer);
	}

	// 攻撃のフェーズ移行
	public void NextPhase(int nextPhase) {
		attackPhase = nextPhase;
	}


	// Use this for initialization
	void Start( ) {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>( );

		// 画像を透明に
		setColor = enemySprite.color;
		setColor.a = 0;
		enemySprite.color = setColor;
		// Collider取得、当たり判定をOFFに
		getCollider =  GetComponent<BoxCollider2D>( );
		getCollider.enabled = false;
		// 待機時と攻撃時それぞれのCollierサイズ。画像変えたら数値変更してください
		neutralCollider = new Vector3(5.9f, 2.8f, 0.0f); // 待機時
		attackCollider = new Vector3(7.3f, 2.4f, 0.0f); // 攻撃時
		// 初期位置保存
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
		endFlag = false;

		attackPhase = 0;
		atkCnt = 0;

		waitTime = 2.0f;

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
						// 透明状態が完全に溶けたら当たり判定をONに
						if(setColor.a > 1) {
							setColor.a = 1;
							getCollider.enabled = true;
						}
						enemySprite.color = setColor;
					}

					// 攻撃の0.5秒前に画像を攻撃用に変更
					if(changeFlag && waitTime <= 0.5f) {
						// 画像を攻撃用に切り替え、それに伴い当たり判定も更新
						enemySprite.sprite = SpriteList[1];
						getCollider.size = attackCollider;
						changeFlag = false;
					}
					// 待機時間0でフェーズ移行。攻撃開始
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
							transform.Translate(attackSpeed * speedGear * Time.deltaTime * timeAdjust);

							// 中央〜初期位置の3倍の距離を移動したら方向転換
							// ３倍進めば画面外だろ！という意図なので、ちゃんと画面の端に配置してください、画面内で処理が発生するとワープします
							if(Mathf.Abs(transform.position.x - startPos.x) > Mathf.Abs(startPos.x * 3.0f)) {
								transform.position = new Vector3(transform.position.x, startPos.y + getCollider.size.x * 0.8f, 0.0f); // 位置を少し高めに変更
								transform.Rotate(0.0f, 0.0f, 180.0f); // 画像半回転
								atkCnt++;
							}
							// 方向転換後、中央〜初期位置の1.5倍の距離まで移動したら処理終了。上と同じく中央に配置すると途中で止まって消える
							// 左右どちらからスタートしても対応するようにしているが、もっとスマートな処理が絶対ある、思いつけない
							else if(atkCnt != 0 && ( (startPos.x > 0 && transform.position.x > startPos.x * 1.5f) || (startPos.x < 0 && transform.position.x < startPos.x * 1.5f) ) ) {
								// フェーズを待機モードに変更。各種設定を初期値に
								attackPhase = 0;
								atkCnt = 0;
								waitTime = 2.0f;
								isAbleToAttack = false;
								changeFlag = true;

								// 初期位置に戻す
								transform.position = startPos;
								transform.Rotate(new Vector3(0.0f, 0.0f, -180.0f) );

								// 画像を待機状態に戻して透明にし当たり判定をOFFに
								enemySprite.sprite = SpriteList[0];
								getCollider.size = neutralCollider;
								getCollider.enabled = false;

								setColor.a = 0;
								enemySprite.color = setColor;

								// 終了フラグがONならばポーズ、でなければ右手へフェーズ変更の命令
								if(endFlag) {
									enemyPauseFlag = true;
								}
								// 現在のギア速度でフェーズを判断して右手にフェーズ変更命令を出しているが、これではフェーズ数や挙動に限界がある
								// switchに対応させる方法をパッと思いつかなかったのでゴリ押したが、enumとかで管理するべきそうすべき
								// ボスは左右の手を同時に配置する前提だが、念のために右手が配置してない場合の例外処理も追加
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
