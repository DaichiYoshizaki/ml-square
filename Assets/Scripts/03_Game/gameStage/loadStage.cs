using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class loadStage : MonoBehaviour {
	void stageLoad(int stageIndex, stageData editItem){
		
		stageData item = ScriptableObject.CreateInstance<stageData>();
		Resources.Load ("stageData/stage" + stageIndex);
	}
	// Use this for initialization
	void Awake () {
		
	}
}
