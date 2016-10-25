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
				int c = int.Parse(CheckString); 
				for( int v = 0; v < 3; v++ )
				{
					if( ManagerSelectStage.ItemAcquisitionRecord [(c-1)*3+v] == false )
					{
						ManagerSelectStage.ItemAcquisitionRecord [(c-1)*3+v] = true;
						break;
					}
				}
				Debug.Log (c);
				break;
			}
			// Optionの場合
			if (CheckString == "Option") 
			{
				Debug.Log (Name);
				break;
			}
			// EnemyListの場合
			if (CheckString == "EnemyList") 
			{
				Debug.Log (Name);
				break;
			}

		}

	}
}
