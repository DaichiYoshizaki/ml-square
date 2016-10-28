using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {
	private GameObject player;
	private GameObject gameCamera;
	private List<GameObject> stages = new List<GameObject>{null, null, null};
	private List<GameObject> playerSpawn = new List<GameObject>{null, null, null};
	static public int currentStageIndex = 0;
	public bool isDebugMode = false;
	public int stage = 0;
	public int AreaSelectIndex = 0;
	static public int areaSelectIndex;
	public bool isTutorial = false;
	static public bool IsTutorial = false;
	static public bool isStageRespawn = false;
	static public int tweetStageIndex = 0;

	void Awake(){
		IsTutorial = isTutorial;
		if (isDebugMode) {
			ManagerSelectStage.TheCurrentlySelectStageID = AreaSelectIndex;
		}
		loadStage.Load (ManagerSelectStage.TheCurrentlySelectStageID - 1);
		currentStageIndex = tweetStageIndex;
	}

	// Use this for initialization
	void Start () {



		stages [0] = GameObject.Find ("PauseObjects/area/gameStage1");
		stages [1] = GameObject.Find ("PauseObjects/area/gameStage2");
		stages [2] = GameObject.Find ("PauseObjects/area/gameStage3");

		playerSpawn [0] = GameObject.Find ("gameStage1/spawnPoint");
		playerSpawn [1] = GameObject.Find ("gameStage2/spawnPoint");
		playerSpawn [2] = GameObject.Find ("gameStage3/spawnPoint");

		player = GameObject.Find ("PauseObjects/gamePlayer");

		gameCamera = GameObject.Find ("gameCamera");

		player.transform.position = playerSpawn [currentStageIndex].transform.position;


		SoundManager.Instance.PlayBGM (1);
		if (ManagerSelectStage.TheCurrentlySelectStageID == 7) {
			SoundManager.Instance.StopBGM ();
			SoundManager.Instance.PlayBGM (2);
		}

		if (isDebugMode) {
			player.transform.position = playerSpawn [areaSelectIndex].transform.position;
			gameCamera.transform.position = new Vector3 (stages [areaSelectIndex].transform.position.x, gameCamera.transform.position.y, gameCamera.transform.position.z);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	}
}
