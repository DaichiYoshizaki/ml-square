using UnityEngine;
using System.Collections;

public class returnGameSelectButton : MonoBehaviour {
	public void OnClick(){
		Fade.instance.FadeOut ("03_Game", 3f, 3f);
	}
}
