using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class s_ButtonFunctionOnOff : MonoBehaviour {


	string StageName; //ステージ

	// Update is called once per frame
	void Update ()
	{
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
			int ID = int.Parse( ManagerSelectStage.LatestCaptureStage.Substring (5, 2) );
			if (i <= ID) 
			{
				Button button = GameObject.Find (StageName).GetComponent<Button> ();
				button.interactable = true;
			} 
			else 
			{
				Button button = GameObject.Find (StageName).GetComponent<Button> ();
				button.interactable = false;
			}

		}
	
	}// update end
}
