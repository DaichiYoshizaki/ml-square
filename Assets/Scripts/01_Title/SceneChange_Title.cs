﻿using UnityEngine;
using System.Collections;

public class SceneChange_Title : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			Fade.instance.FadeOut("02_SelectStage", 3, 1);
		}
	}
}