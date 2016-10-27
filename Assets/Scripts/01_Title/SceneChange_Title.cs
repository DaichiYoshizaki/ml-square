using UnityEngine;
using System.Collections;

public class SceneChange_Title : MonoBehaviour {
	// Use this for initialization
	void Start () {
		SoundManager.Instance.PlayBGM (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			SoundManager.Instance.PlaySE (0);
			SoundManager.Instance.StopBGM ();
			Fade.instance.FadeOut("03_Game", 3, 1);
		}
	}
}
