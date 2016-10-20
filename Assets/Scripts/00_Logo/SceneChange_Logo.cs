using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChange_Logo : MonoBehaviour {
	float second = 0.0f;
	GameObject fade;

	// Use this for initialization
	void Start () {
		fade = GameObject.Find("Fade");
	}
	
	// Update is called once per frame
	void Update () {
		second += Time.deltaTime;

		if(Input.touchCount > 0 || second > 3.0f || Input.GetMouseButtonDown(0)) {
			fade.SendMessage("FadeOut", "01_Title");
		}
	}
}
