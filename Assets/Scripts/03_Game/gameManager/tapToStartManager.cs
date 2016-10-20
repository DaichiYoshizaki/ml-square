using UnityEngine;
using System.Collections;

public class tapToStartManager : MonoBehaviour {
	public GameObject tapToStartCanvas;
	private Canvas canvas;

	// Use this for initialization
	void Start () {
		pauser.Pause ();
		canvas = tapToStartCanvas.GetComponent<Canvas> ();
		canvas.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//タッチがあるか動画

		if (Input.GetMouseButtonDown (0)) {
			canvas.enabled = false;
			pauser.Resume ();
		}

		if (Input.touchCount > 0){

			Touch touch = Input.GetTouch(0);

			//タッチしたら
			if(touch.phase == TouchPhase.Began){
				canvas.enabled = false;
				pauser.Resume ();
			}


		}

	}
	static public void showTapToStart(){
		
	}
}
