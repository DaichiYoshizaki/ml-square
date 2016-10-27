using UnityEngine;
using System.Collections;

public class returnGameSelectButton : MonoBehaviour {
	public void OnClick(){
		SoundManager.Instance.PlaySE (0);
		Fade.instance.FadeOut ("02_SelectStage", 3f, 3f);
	}
}
