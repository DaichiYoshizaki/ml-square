using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {
	private GameObject timerObject;
	private GameObject player;
	private GameObject gameCamera;
	private List<GameObject> stages = new List<GameObject>{null, null, null};
	private List<GameObject> playerSpawn = new List<GameObject>{null, null, null};
	private timer timer;
	static public int currentStageIndex = 0;
	public bool isDebugMode = false;
	public int stage = 0;
	public int AreaSelectIndex = 0;
	static public int areaSelectIndex;
	void Awake(){
		
		loadStage.Load (AreaSelectIndex);
	}

	// Use this for initialization
	void Start () {

		currentStageIndex = 0;

		stages [0] = GameObject.Find ("PauseObjects/area/gameStage1");
		stages [1] = GameObject.Find ("PauseObjects/area/gameStage2");
		stages [2] = GameObject.Find ("PauseObjects/area/gameStage3");

		playerSpawn [0] = GameObject.Find ("gameStage1/spawnPoint");
		playerSpawn [1] = GameObject.Find ("gameStage2/spawnPoint");
		playerSpawn [2] = GameObject.Find ("gameStage3/spawnPoint");

		player = GameObject.Find ("PauseObjects/gamePlayer");

		timerObject = GameObject.Find ("PauseObjects/timerManager");

		gameCamera = GameObject.Find ("gameCamera");

		player.transform.position = playerSpawn [0].transform.position;

		timer = timerObject.GetComponent<timer> ();

		if (isDebugMode) {
			player.transform.position = playerSpawn [stage].transform.position;
			gameCamera.transform.position = new Vector3 (stages [stage].transform.position.x, gameCamera.transform.position.y, gameCamera.transform.position.z);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	}
}
