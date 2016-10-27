using UnityEngine;
using System.Collections;

public class s_TransitionStage : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	// ボタンを押した時の処理
	public void onClick( string Name )
	{
		
		string CheckString;	// 文字列比較用
		const int STRING_NUM_MIN = 5;	// チェックする文字列の最小値
		const int STRING_NUM_MAX = 10;  // チェックする文字列の最大値

		// 文字数分繰り返す
		for( int i = STRING_NUM_MIN; i <  STRING_NUM_MAX; i++ )
		{
			//文字列取得
			CheckString = Name.Substring( 0, i );

			// Stageの場合
			if (CheckString == "Stage") 
			{
				// 選択したステージIDの文字列抽出
				CheckString = Name.Substring (5, 2);

				// 現在選択しているステージのIDの更新
				ManagerSelectStage.TheCurrentlySelectStageID = int.Parse(CheckString); 

				// 最新攻略ステージ更新
				CheckCaptureLatestStage( Name );
				// フェード
				Fade.instance.FadeOut ("03_Game", 3,3 );

				break;
			}
			// Optionの場合
			if (CheckString == "Option") 
			{
				// フェード
				//Fade.instance.FadeOut ("03", 3,3 );
				break;
			}
			// EnemyListの場合
			if (CheckString == "EnemyList") 
			{
				// フェード
				Fade.instance.FadeOut ("04_EnemyList", 3,3 );
				break;
			}

		}

	}

	// 最新攻略ステージがどうかチェック
	void CheckCaptureLatestStage( string ID_Name )
	{
		// IDから番号抽出
		int CheckID_Name = int.Parse( ID_Name.Substring (5, 2) );
		int CheckLatestCaptureStage = int.Parse( ManagerSelectStage.LatestCaptureStage.Substring (5, 2));

		// ステージが今のステージより最新攻略ステージなら
		if (CheckLatestCaptureStage < CheckID_Name) 
		{
			ManagerSelectStage.LatestCaptureStage = ID_Name;
			Debug.Log (ManagerSelectStage.LatestCaptureStage);
		}
	}
}
