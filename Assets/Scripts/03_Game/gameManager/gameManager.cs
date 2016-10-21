using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	public GameObject timerObject;
	private timer timer;
	static public int currentStageIndex = 0;

	// Use this for initialization
	void Awake () {
		currentStageIndex = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
	}
}
