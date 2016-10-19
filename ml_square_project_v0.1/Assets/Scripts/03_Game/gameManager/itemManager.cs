using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemManager : MonoBehaviour {

	static private List<bool> getItemOnStageIndex;

	static void GetItem (){
		getItemOnStageIndex[gameManager.currentStageIndex] = true;
	}

	// Use this for initialization
	void Awake () {
		getItemOnStageIndex.Clear ();
	}

}
