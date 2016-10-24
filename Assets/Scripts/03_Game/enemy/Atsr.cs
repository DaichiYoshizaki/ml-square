using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atsr : MonoBehaviour {
	private float rotSpeed = -5; // 回転速度
	private SpriteRenderer enemySprite; // スプライト情報取得用
	public List<Sprite> SpriteList; // スプライトリスト取得用
	static bool enemyPauseFlag = false; // ポーズ状態フラグ

	// ポーズ状態のON/OFF
	public void EnemyPauseChange( ) {
		enemyPauseFlag = !enemyPauseFlag;
	}

	//プロパティ--------------------------------
	public float RotSpeed{
		set{rotSpeed = value;}
		get{return rotSpeed;}
	}
	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
		enemySprite = gameObject.transform.FindChild ("enemySprite").GetComponent<SpriteRenderer>();
		enemySprite.sprite = SpriteList[0];
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		// ポーズ状態では更新しない
		if(!enemyPauseFlag) {
			transform.Rotate(new Vector3(0, 0, 1), rotSpeed);
		}
	}
}
