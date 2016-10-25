using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameClearManager : MonoBehaviour {
	private List<GameObject> stages;
	private List<GameObject> playerSpawn;
	public GameObject player;
	public GameObject camera;
	public GameObject craneObject;
	public GameObject enemyManager;
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
	private bool isAbleToCameraMove = false;

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
	void Awake () {
		isStageClear = false;
		isAreaClear = false;
		isAbleToCameraMove = false;
		playerSpawn = new List<GameObject>{null, null, null};

		playerSpawn [0] = GameObject.Find ("gameStage1/spawnPoint");
		playerSpawn [1] = GameObject.Find ("gameStage2/spawnPoint");
		playerSpawn [2] = GameObject.Find ("gameStage3/spawnPoint");

		//awake for Canvas
		clearPanelAwake ();
		crane = craneObject.GetComponent<crane> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isStageClear && !isAreaClear) {
			gameManager.currentStageIndex++;
			GameObject dummyPlayer = (GameObject)Instantiate(player, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			Destroy (dummyPlayer.GetComponent<playerMover> ());
			dummyPlayer.GetComponent<Rigidbody2D> ().isKinematic = true;
			dummyPlayer.GetComponent<Collider2D> ().isTrigger = true;
			dummyPlayer.name = "dummyPlayer";
			dummyPlayer.transform.parent = null;
			player.transform.position = playerSpawn [gameManager.currentStageIndex].transform.position;
			crane.IsStartCrane = true;
			pauser.Pause ();
			enemyManager.GetComponent<Enemy>( ).PauseEnemy(gameManager.currentStageIndex - 1);
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
			isAbleToCameraMove = true;
		} else if (crane.IsEndCrane && isAreaClear) {
			open ();
		}

		if (isAbleToCameraMove) {
			camera.transform.Translate (Vector3.right * 10.0f * Time.deltaTime);
			if (camera.transform.position.x > playerSpawn [gameManager.currentStageIndex].transform.position.x) {
				camera.transform.position = new Vector3 (playerSpawn [gameManager.currentStageIndex].transform.position.x, camera.transform.position.y, 0f);
				pauser.Resume ();
				pauser.Pause ();
				player.GetComponent<playerMover> ().IsAwake = true;
				enemyManager.GetComponent<Enemy>( ).PauseEnemy(gameManager.currentStageIndex);
				tapToStartManager.showTapToStart ();
				timer.StartTimer ();
				isAbleToCameraMove = false;
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
