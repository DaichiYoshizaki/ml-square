/*******************************************************************************************************************************************************
 * アツラクラス
 * 
 * ただその場で回ってるだけ
*******************************************************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atsr : Enemy {
	private float rotSpeed = -5; // 回転速度
	private CircleCollider2D getCollider; // Collider取得用


	// Use this for initialization
	void Start( ) {
		// Collider取得
		getCollider = GetComponent<CircleCollider2D>( );
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

			// 回転
			transform.Rotate(new Vector3(0, 0, 1), rotSpeed);
		}
		else if(getCollider.enabled) {
			// 当たり判定OFF
			getCollider.enabled = false;
		}
	}
}
