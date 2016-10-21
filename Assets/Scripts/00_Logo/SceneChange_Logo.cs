using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChange_Logo : MonoBehaviour {
	float second = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		second += Time.deltaTime;

		if(Input.touchCount > 0 || second > 3.0f || Input.GetMouseButtonDown(0)) {
			Fade.instance.FadeOut("01_Title", 2, 1);
		}
	}
}
