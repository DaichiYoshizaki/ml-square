using UnityEngine;
using System.Collections;

public class pauseBottan : MonoBehaviour{
    public void OnClick(){
        pauser.Pause();
		SoundManager.Instance.PlaySE (0);
		menuManager.open ();
    }
}
