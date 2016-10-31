using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

public class stageAllCreate{
	[MenuItem("StageEdit/allCreate")]
	static public void allCreate(){
		string[] guids = new string[99];
		string path = AssetDatabase.GUIDToAssetPath (guids [0]);
		stageData obj = AssetDatabase.LoadAssetAtPath<stageData> (path);

		for(int allCnt = 0; allCnt < 99; allCnt++){
				stageData sd = new stageData{ };
			if (Resources.Load ("OriginalStageData/stage" + allCnt.ToString ())) {
					string a = "Resources/OriginalStageData/stage" + allCnt.ToString ();
					AssetDatabase.DeleteAsset (a);
				}
				saveStageData.CreateAsset (sd, allCnt);
				AssetDatabase.Refresh ();
		}

	}

}