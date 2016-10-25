using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemManager : MonoBehaviour {

	static private List<bool> getItemOnStageIndex;

	static public void GetItem (){
		getItemOnStageIndex[gameManager.currentStageIndex] = true;
	}

	// Use this for initialization
	void Start () {
		getItemOnStageIndex = new List<bool>{ false , false, false};
	}

}
