using UnityEngine;
using System.Collections;

public class stageRetryButton : MonoBehaviour {
	public void OnClick(){
		Fade.instance.FadeOut ("03_Game", 3, 3);
	}
}
