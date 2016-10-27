using UnityEngine;
using System.Collections;

public class stageRetryButton : MonoBehaviour {
	public void OnClick(){
		SoundManager.Instance.PlaySE (0);
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.StopME();
		Fade.instance.FadeOut ("03_Game", 3, 3);
	}
}
