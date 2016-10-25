using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atsr : Enemy {
	private float rotSpeed = -5; // 回転速度

	//プロパティ--------------------------------
	public float RotSpeed{
		set{rotSpeed = value;}
		get{return rotSpeed;}
	}
	//プロパティ終わり----------------------------


	// Use this for initialization
	void Start () {
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
