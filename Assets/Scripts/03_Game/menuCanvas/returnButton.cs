using UnityEngine;
using System.Collections;

public class returnButton : MonoBehaviour {
	public void OnClick(){
		SoundManager.Instance.PlaySE (0);
		menuManager.close ();
	}
}
