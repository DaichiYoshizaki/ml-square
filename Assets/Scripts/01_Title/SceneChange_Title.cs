using UnityEngine;
using System.Collections;

public class SceneChange_Title : MonoBehaviour {
	GameObject fade;

	// Use this for initialization
	void Start () {
		fade = GameObject.Find("Fade");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			fade.SendMessage("FadeOut", "02_SelectStage");
		}
	}
}
