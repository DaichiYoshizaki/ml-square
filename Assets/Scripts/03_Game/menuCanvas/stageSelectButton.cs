using UnityEngine;
using System.Collections;

public class stageSelectButton : MonoBehaviour {
	public void OnClick(){
		SoundManager.Instance.PlaySE (0);
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.StopME ();
		Fade.instance.FadeOut ("02_SelectStage", 3, 3);
	}
}
