﻿using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	public GameObject timerPrefab;
	private timer timer;
	static public int currentStageIndex = 0;

	// Use this for initialization
	void Awake () {
		currentStageIndex = 0;
		timer = GetComponent<timer> ();
	}

	// Update is called once per frame
	void Update () {
		if (timer.CountTime < 0) {
			currentStageIndex++;
		}
	}
}
