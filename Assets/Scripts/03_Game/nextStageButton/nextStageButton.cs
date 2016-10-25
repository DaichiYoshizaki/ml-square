using UnityEngine;
using System.Collections;

public class nextStageButton : MonoBehaviour {
	public void OnClick(){
		ManagerSelectStage.TheCurrentlySelectStageID++;
		Fade.instance.FadeOut ("03_Game", 3, 3);
	}
}
