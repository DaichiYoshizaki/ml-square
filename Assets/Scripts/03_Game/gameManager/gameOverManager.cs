using UnityEngine;
using System.Collections;

public class gameOverManager : MonoBehaviour {
	public GameObject gameOverCanvas;
	static private Canvas canvas;
	private Vector3 panelDefaultScale;
	static public bool isShowGameOver = false;
	static public bool isOpening = false;
	static public bool isClosing = false;
	public GameObject panelObject;
	private RectTransform panelRt;

	// Use this for initialization
	void Awake () {
		isShowGameOver = false;
		isOpening = false;
		isClosing = false;
		canvas = gameOverCanvas.GetComponent<Canvas> ();
		panelRt = panelObject.GetComponent<RectTransform> ();
		panelDefaultScale = panelRt.transform.localScale;
		panelRt.transform.localScale = new Vector3(0f, 0f, 0f);
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpening) {
			panelRt.transform.localScale += panelDefaultScale / 10f;
			if (panelRt.transform.localScale.x > panelDefaultScale.x) {
				panelRt.transform.localScale = panelDefaultScale;
				isOpening = false;
				isShowGameOver = true;
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
	}
	static public void open(){
		if (!isOpening && !isClosing) {
			isOpening = true;
			canvas.enabled = true;
		}
	}
	static public void close(){
		if (!isOpening && !isClosing)
			isClosing = true;	
	}

}
