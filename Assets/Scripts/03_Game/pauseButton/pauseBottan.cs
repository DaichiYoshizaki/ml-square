using UnityEngine;
using System.Collections;

public class pauseBottan : MonoBehaviour{
    public void OnClick(){
        pauser.Pause();
		menuManager.open ();
    }
}
