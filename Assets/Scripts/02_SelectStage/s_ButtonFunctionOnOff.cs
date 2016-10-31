using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 攻略した最新ステージ以上のステージを選択できないようにする。
public class s_ButtonFunctionOnOff : MonoBehaviour 
{
	string StageName; //ステージ

	// Update is called once per frame
	void Update ()
	{
		// ステージの数分繰り返す
		for (int i = 1; i < 35; i++) 
		{
			// 
			StageName = "Stage";

			// Stage1~9
			if (i < 10) 
			{
				StageName = StageName + "0" + i.ToString ();
			} 
			// Stage10~33
			else if (i >= 10) 
			{
				StageName = StageName + i.ToString ();
			}

			// スデージ番号をintに変換
			int ID = int.Parse( ManagerSelectStage.LatestCaptureStage.Substring (5, 2) );

			// ステージの選択できるかどうか切り替え
				if (i <= ID) // 攻略した最新ステージ以下の場合
				{
					Button button = GameObject.Find (StageName).GetComponent<Button> ();
					button.interactable = true;
				} 
				else // 攻略した最新ステージ以上の場合
				{
					Button button = GameObject.Find (StageName).GetComponent<Button> ();
					button.interactable = false;
				}
		}
	
	}// update end
}
