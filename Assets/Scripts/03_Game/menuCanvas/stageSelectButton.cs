using UnityEngine;
using System.Collections;

public class stageSelectButton : MonoBehaviour {
	public void OnClick(){
		Fade.instance.FadeOut ("02_SelectStage", 3, 3);
	}
}
