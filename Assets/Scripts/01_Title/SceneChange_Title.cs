using UnityEngine;
using System.Collections;

public class SceneChange_Title : MonoBehaviour {
	// Use this for initialization
	private GameObject obj;
	void Start () {
		SoundManager.Instance.PlayBGM (0);
		obj = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			SoundManager.Instance.PlaySE (0);
			SoundManager.Instance.StopBGM ();
			Fade.instance.FadeOut("02_SelectStage", 3, 1);
			obj.GetComponent<playerJump> ().IsAbleToJump = true;
		}
	}
}
