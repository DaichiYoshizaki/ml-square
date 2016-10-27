using UnityEngine;
using System.Collections;

public class tapToStartManager : MonoBehaviour {
	public GameObject tapToStartCanvas;
	static private Canvas canvas;
	private Vector3 panelDefaultScale;
	static private bool isShowTapToStart = false;
	static private bool isOpening = false;
	static private bool isClosing = false;
	public GameObject panelObject;
	private RectTransform panelRt;

	// Use this for initialization
	void Start () {
		isShowTapToStart = false;
		isOpening = false;
		isClosing = false;
		canvas = tapToStartCanvas.GetComponent<Canvas> ();
		panelRt = panelObject.GetComponent<RectTransform> ();
		panelDefaultScale = panelRt.transform.localScale;
		canvas.enabled = true;
		isOpening = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpening) {
			panelRt.transform.localScale += panelDefaultScale / 10f;
			if (panelRt.transform.localScale.x > panelDefaultScale.x) {
				panelRt.transform.localScale = panelDefaultScale;
				isOpening = false;
				isShowTapToStart = true;
				pauser.Pause ();
			}
		}
		if (isClosing) {
			panelRt.transform.localScale -= panelDefaultScale  / 10f;
			if (panelRt.transform.localScale.x < 0.2f) {
				panelRt.transform.localScale = new Vector3(0f, 0f, 0f);
				isClosing = false;
				canvas.enabled = false;
				pauser.Resume ();
			}		
		}
			
		if (isShowTapToStart) {
			if (Input.GetMouseButtonDown (0)) {
				isShowTapToStart = false;
				isClosing = true;
				SoundManager.Instance.PlaySE(0);
			}

			if (Input.touchCount > 0) {

				Touch touch = Input.GetTouch (0);

				//タッチしたら
				if (touch.phase == TouchPhase.Began) {
					isShowTapToStart = false;
					isClosing = true;
					SoundManager.Instance.PlaySE(0);
				}			
			}
		}

	}
	static public void showTapToStart(){
		canvas.enabled = true;
		isOpening = true;
	}
}
