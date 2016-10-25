using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameClearManager : MonoBehaviour {
	private List<GameObject> stages = new List<GameObject>{null, null, null};
	private List<GameObject> playerSpawn = new List<GameObject>{null, null, null};

	private GameObject player;
	public GameObject gameCamera;
	public GameObject craneObject;
	private crane crane;

	static private bool isAreaClear;
	static private bool isStageClear;

	//for canvas
	public GameObject gameClearCanvas;
	static private Canvas canvas;
	private Vector3 panelDefaultScale;
	static public bool isShowGameClear = false;
	static public bool isOpening = false;
	static public bool isClosing = false;
	public GameObject panelObject;
	private RectTransform panelRt;
	private bool isAbleToMove = false;

	static public bool IsAreaClear{
		get{ return isAreaClear; }
	}

	static public void stageClear(){
		isStageClear = true;
		if (gameManager.currentStageIndex == 2) {
			isAreaClear = true;
		}
	}

	// Use this for initialization
	void Start () {
		isStageClear = false;
		isAreaClear = false;
		isAbleToMove = false;
		playerSpawn = new List<GameObject>{null, null, null};

		stages [0] = GameObject.Find ("PauseObjects/area/gameStage1");
		stages [1] = GameObject.Find ("PauseObjects/area/gameStage2");
		stages [2] = GameObject.Find ("PauseObjects/area/gameStage3");

		playerSpawn [0] = GameObject.Find ("gameStage1/spawnPoint");
		playerSpawn [1] = GameObject.Find ("gameStage2/spawnPoint");
		playerSpawn [2] = GameObject.Find ("gameStage3/spawnPoint");

		player = GameObject.Find ("PauseObjects/gamePlayer");

		gameCamera = GameObject.Find ("gameCamera");

		//awake for Canvas
		clearPanelAwake ();
		crane = craneObject.GetComponent<crane> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isStageClear && !isAreaClear) {
			playerSpawn [0] = GameObject.Find ("gameStage1/spawnPoint");
			playerSpawn [1] = GameObject.Find ("gameStage2/spawnPoint");
			playerSpawn [2] = GameObject.Find ("gameStage3/spawnPoint");

			gameManager.currentStageIndex++;
			GameObject dummyPlayer = (GameObject)Instantiate(player, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			Destroy (dummyPlayer.GetComponent<playerMover> ());
			dummyPlayer.GetComponent<Rigidbody2D> ().isKinematic = true;
			dummyPlayer.GetComponent<Collider2D> ().isTrigger = true;
			dummyPlayer.name = "dummyPlayer";
			dummyPlayer.transform.parent = null;
			player.transform.position = playerSpawn [gameManager.currentStageIndex].transform.position;
			player.transform.Translate(new Vector3(0f, 0f, 1f));
			crane.IsStartCrane = true;
			pauser.Pause ();
			isStageClear = false;

		}
		else if (isStageClear && isAreaClear) {
			gameManager.currentStageIndex++;
			GameObject dummyPlayer = (GameObject)Instantiate(player, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			Destroy (dummyPlayer.GetComponent<playerMover> ());
			dummyPlayer.GetComponent<Rigidbody2D> ().isKinematic = true;
			dummyPlayer.GetComponent<Collider2D> ().isTrigger = true;
			dummyPlayer.name = "dummyPlayer";
			dummyPlayer.transform.parent = null;
			Destroy (player);
			crane.IsStartCrane = true;
			pauser.Pause ();
			isStageClear = false;
		}
		if (crane.IsEndCrane && !isAreaClear) {
			crane.IsEndCrane = false;
			isAbleToMove = true;
		} else if (crane.IsEndCrane && isAreaClear) {
			open ();
		}

		if (isAbleToMove) {
			gameCamera.transform.Translate (Vector3.right * 10.0f * Time.deltaTime);
			if (gameCamera.transform.position.x > stages [gameManager.currentStageIndex].transform.position.x) {
				gameCamera.transform.position = new Vector3 (stages [gameManager.currentStageIndex].transform.position.x, gameCamera.transform.position.y, 0f);
				pauser.Resume ();
				pauser.Pause ();
				player.GetComponent<playerMover> ().IsAwake = true;
				tapToStartManager.showTapToStart ();
				timer.StartTimer ();
				isAbleToMove = false;
			}
		}

		//update for Canvas

		clearPanelUpdate ();
	}

	//for canvas

	void clearPanelAwake(){
		isShowGameClear = false;
		isOpening = false;
		isClosing = false;
		canvas = gameClearCanvas.GetComponent<Canvas> ();
		panelRt = panelObject.GetComponent<RectTransform> ();
		panelDefaultScale = panelRt.transform.localScale;
		panelRt.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	void clearPanelUpdate(){
		if (isOpening) {
			panelRt.transform.localScale += panelDefaultScale / 10f;
			if (panelRt.transform.localScale.x > panelDefaultScale.x) {
				panelRt.transform.localScale = panelDefaultScale;
				isOpening = false;
				isShowGameClear = true;
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
