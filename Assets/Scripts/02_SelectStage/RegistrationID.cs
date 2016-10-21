using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// 選択したステージのIDの登録
public class RegistrationID : MonoBehaviour 
{
	public static int TheCurrentlySelectedStageID = -1; // 現在選択しているステージのID

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
		
	// 
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
				TheCurrentlySelectedStageID = int.Parse(CheckString); 
				Debug.Log ("Stage");
				break;
			}
			// Optionの場合
			if (CheckString == "Option") 
			{
				Debug.Log (" Option");
				break;
			}
			// EnemyListの場合
			if (CheckString == "EnemyList") 
			{
				Debug.Log ("EnemyList");
				break;
			}

		}

	}
}
