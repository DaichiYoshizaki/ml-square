using UnityEngine;
using System.Collections;

public class nextStageButton : MonoBehaviour {
	public void OnClick(){
		ManagerSelectStage.TheCurrentlySelectStageID++;
		SoundManager.Instance.PlaySE(0);
		SoundManager.Instance.PlayME(0);
		Fade.instance.FadeOut ("03_Game", 3, 3);
	}
}
