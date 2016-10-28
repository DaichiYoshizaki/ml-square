using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameClearManager : MonoBehaviour {
	private List<GameObject> stages = new List<GameObject>{null, null, null};
	private List<GameObject> playerSpawn = new List<GameObject>{null, null, null};

	private GameObject player;					//find用
	public GameObject gameCamera;				//find用
	public GameObject craneObject;				//find用
	public GameObject enemyManager;				//find用
	private crane crane;						//GetCompornentの入れ物

	static private bool isAreaClear;
	static private bool isStageClear;

	static public bool isAbleToOpen = true;


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
		SoundManager.Instance.PlaySE (5);
		if (gameManager.currentStageIndex == 2) {
			isAreaClear = true;
			SoundManager.Instance.StopBGM ();
			SoundManager.Instance.PlayME (1);
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

			menuManager.isAbleOpen = false;

			gameManager.currentStageIndex++;
			GameObject dummyPlayer = (GameObject)Instantiate(player, player.transform.position, new Quaternion(0f, 0f, 0f, 0f));
			Destroy (dummyPlayer.GetComponent<playerMover> ());
			dummyPlayer.GetComponent<Rigidbody2D> ().isKinematic = true;
			dummyPlayer.GetComponent<Collider2D> ().isTrigger = true;
			dummyPlayer.name = "dummyPlayer";
			dummyPlayer.transform.parent = null;
			player.GetComponent<playerMover> ().playerVisualReset ();
			player.transform.position = playerSpawn [gameManager.currentStageIndex].transform.position;
			player.transform.Translate(new Vector3(0f, 0f, 1f));
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
			player.GetComponent<playerMover> ().playerVisualReset ();
			Destroy (player);
			crane.IsStartCrane = true;
			pauser.Pause ();
			enemyManager.GetComponent<Enemy>( ).PauseEnemy(gameManager.currentStageIndex - 1);
			isStageClear = false;
		}
		if (crane.IsEndCrane && !isAreaClear) {
			crane.IsEndCrane = false;
			isAbleToMove = true;
		} else if (crane.IsEndCrane && isAreaClear) {
			open ();


			//チュートリアルなら
			if (!gameManager.IsTutorial) {
				for (int i = 0; i < 3; i++) {
					ManagerSelectStage.ItemAcquisitionRecord [(ManagerSelectStage.TheCurrentlySelectStageID - 1) * 3 + i] = itemManager.getItemOnStageIndex [i];
				}

				int stageNum = ManagerSelectStage.TheCurrentlySelectStageID;

				ManagerSelectStage.LatestCaptureStage = "Stage" + string.Format ("{0:D2}", stageNum);

				GameSaveDataOperation.SaveGameSaveDataAll ();

				GameSaveDataOperation.LoadGameSaveDataAll ();

			}

			crane.IsEndCrane = false;
		}

		if (isAbleToMove) {
			gameCamera.transform.Translate (Vector3.right * 10.0f * Time.deltaTime);
			if (gameCamera.transform.position.x > stages [gameManager.currentStageIndex].transform.position.x) {
				gameCamera.transform.position = new Vector3 (stages [gameManager.currentStageIndex].transform.position.x, gameCamera.transform.position.y, 0f);
				pauser.Resume ();
				pauser.Pause ();
				player.GetComponent<playerMover> ().IsAwake = true;
				enemyManager.GetComponent<Enemy>( ).PauseEnemy(gameManager.currentStageIndex);
				tapToStartManager.showTapToStart ();
				timer.StartTimer ();
				isAbleToMove = false;
				isAbleToOpen = false;
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
			menuManager.close ();
			isOpening = true;
			canvas.enabled = true;
		}
	}
	static public void close(){
		if (!isOpening && !isClosing) {
			isClosing = true;
			SoundManager.Instance.StopME ();
		}
	}

}
