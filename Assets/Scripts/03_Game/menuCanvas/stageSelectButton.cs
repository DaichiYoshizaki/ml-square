using UnityEngine;
using System.Collections;

public class stageSelectButton : MonoBehaviour {
	public void OnClick(){
		Fade.instance.FadeOut ("01_Title", 3, 3);
	}
}
