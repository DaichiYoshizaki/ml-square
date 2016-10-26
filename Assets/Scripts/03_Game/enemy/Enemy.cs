using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	private List<List<GameObject> > enemyList = new List<List<GameObject> >( );
	protected bool enemyPauseFlag = true; // ポーズ状態フラグ

	private void FindEnemy( ) {
		List<GameObject> getEnemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy") );
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage1") );
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage2") );
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage3") );
	}

	public void PauseEnemy(int stageNum) {
		foreach(GameObject enemy in enemyList[stageNum]) {
			enemy.GetComponent<Enemy>( ).EnemyPauseChange( );
		}
	}

	// ポーズ状態のON/OFF
	private void EnemyPauseChange( ) {
		enemyPauseFlag = !enemyPauseFlag;
	}

	// Use this for initialization
	void Start( ) {
		FindEnemy( );
		PauseEnemy(gameManager.currentStageIndex);
	}
	
	// Update is called once per frame
	void Update( ) {
	
	}
}
