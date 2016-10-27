using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trignale_green : Enemy {
	private bool isFacingRight = true; // 向いている方向判定
	private float moveSpeed = 0.15f; // 移動速度
	private bool isAbleToMove = true; // 移動可能か否か（衝突時に使用）
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	public LayerMask groundLayer; // 障害物レイヤ
	public LayerMask wallLayer; // 画面端レイヤ
	private float waitTime = 0; // 衝突時の待機時間
	private bool changeFlag = false; // 衝突時の方向転換判定
	private GameObject playerMover; // プレイヤー情報取得用
	private Vector3 oldPosition; // 前回位置保存用
	private Vector3 colSize; // Colliderのサイズ取得用
	private Vector2 colOffset; // Colliderのoffset取得用

	// 横方向当たり判定
	private bool IsHorizontalCollied(){
		if (isFacingRight) {
			// 障害物と画面端の両方で当たり判定
			if(Physics2D.Linecast(transform.position, transform.position + transform.right * (colSize.x * 0.5f + colOffset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position + transform.right * (colSize.x * 0.5f + colOffset.x), groundLayer) )
				return true;
		}
		else {
			if(Physics2D.Linecast(transform.position, transform.position - transform.right * (colSize.x * 0.5f - colOffset.x), wallLayer) ||
				Physics2D.Linecast(transform.position, transform.position - transform.right * (colSize.x * 0.5f - colOffset.x), groundLayer) )
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
		colSize =  GetComponent<BoxCollider2D>( ).bounds.size;
		colOffset = GetComponent<BoxCollider2D>( ).offset;
		// プレイヤーの位置から初期の向きを設定
		IsPlayerRightside( );
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			// 障害物か画面端に衝突したらしばらく動きを止める
			// もしくは、移動処理を行っているのに前フレームから変化がなければ、移動を中断
			if((IsHorizontalCollied( ) || oldPosition == transform.position) && isAbleToMove) {
				ChangeFace( );
				waitTime = 2.0f;
				changeFlag = true;
				isAbleToMove = false;
				oldPosition.x += 1; // 衝突後の待機状態が終わった時点でoldPosition == positionを満たしてしまうため、数値をずらしておく
				// SE再生
				SoundManager.Instance.PlaySE(3);
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
			}
			else {
				// 衝突後の行動不可時間進行
				waitTime -= Time.deltaTime;
			}
		}
	}
}
