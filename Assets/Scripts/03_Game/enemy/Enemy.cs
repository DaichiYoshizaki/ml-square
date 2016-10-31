/*******************************************************************************************************************************************************
 * エネミー基底クラス
 * 
 * エネミー処理全般担当　秋間雄太
 * 
 * 現在のステージのエネミーのみを動かすため、一括でポーズのON/OFFを切り替えるのが主な役目
 * 複数のエネミーで同名の変数を用いているケースは多いが、利用しているのが半数程度であったり、名前は同じでも型が違ったりするため基底では持たせにくい
 * 
 ***********************************************************************************************************************************
 * 注）
 * 　各エネミーの各種処理にかけられている deltaTime * timeAdjust は応急処置
 * 　部分的にdeltaTimeを使用せずに組んでしまったため、処理速度の違いで端末によって挙動に差が生じていたのでdeltaTimeを追加
 * 　既に敵の制作は終わりステージも作り始めてしまっていたため、ステージを作っていた秋間の環境下で同じ動きになるように数値を調整した結果timeAdjustが50になった
 * 　ここを変更するとバランスやステージ構成を再調整する必要が生じる可能性があるため注意
 * 　後付けの雑作業なので deltaTime * 50 とマジックナンバーが残っていたり、使用すべき場所でdeltaTimeが用いられてない可能性も残っています、ごめんなさい
 * 
 ***********************************************************************************************************************************
 * 
 * 一部の敵に実装されているめり込み対策はクソみたいなwhile文を使用しているため、フリーズ系統の最悪なバグの温床
 * Rigidbodyをよく理解できずに自分で当たり判定とめり込み判定を作ったためこうなったが、RigidbodyのON/OFFでもっと上手く作れたのではないか
 * 発想はあるが時間切れのため実装する余裕なし、無念
 * 
 ***********************************************************************************************************************************
 * どうでもいい備考
 * 　敵の名前は記号のアナグラム　triangle -> trignale トリゴナル
 * 							square   -> srauqe	 スラーク
 * 							circle	 -> riccle	 リックル
 * 							star	 -> atsr	 アツラ
 * 　　トリゴナルは初期企画書からいる内田さん命名のキャラなため勝手に想像してこうなった。なぜ内田さんに相談しなかったのか、コミュ症死すべし
*******************************************************************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	private List<List<GameObject> > enemyList = new List<List<GameObject> >( ); // 敵リスト
	protected bool enemyPauseFlag = true; // ポーズ状態フラグ。開始時は全ての敵がポーズ状態で停止している
	protected const int timeAdjust = 50;

	private void FindEnemy( ) {
		List<GameObject> getEnemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy") );
		// 各ステージの敵情報を取得し、ステージ毎に別の配列の収納
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage1") );
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage2") );
		enemyList.Add(getEnemy.FindAll(x => x.transform.parent.name == "gameStage3") );
	}

	// 指定されたステージに配置されている敵のポーズ状態を切り返す（ONをOFFに、OFFをONに）
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
		// 起動時に各ステージの敵情報を取得し、現在のステージの敵のポーズを解除する
		FindEnemy( );
		PauseEnemy(gameManager.currentStageIndex);
	}
	
	// Update is called once per frame
	void Update( ) {
	
	}
}
