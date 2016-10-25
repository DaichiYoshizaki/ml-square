using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class loadStage : MonoBehaviour {
	static public GameObject[] prefabs;
	static public void Load(int stageIndex){
		prefabs = null;
		prefabs = Resources.LoadAll <GameObject>("Prefabs");

		Dictionary<string, GameObject> prefabsDic = new Dictionary<string, GameObject>();

		foreach(GameObject prefab in prefabs){
			prefabsDic.Add (prefab.name, prefab);
		}

		stageData item = Resources.Load ("stageData/stage" + stageIndex) as stageData;

		foreach (EnemyDetail ed in item.entryEnemy){
			GameObject obj;
			GameObject[] stages = new GameObject[]{null, null, null};
			stages [0] = GameObject.Find ("PauseObjects/area/gameStage1");
			stages [1] = GameObject.Find ("PauseObjects/area/gameStage2");
			stages [2] = GameObject.Find ("PauseObjects/area/gameStage3");
			if (ed.prefabName == "gamePlayer" && GameObject.Find ("gamePlayer")) {
				GameObject player = GameObject.Find ("gamePlayer");
				Destroy (player);
			}
			if (ed.prefabName == "spawnPoint" && GameObject.Find ("PauseObjects/area/gameStage" + (ed.stageIndex + 1).ToString() + "/spawnPoint")) {
				GameObject sp = GameObject.Find ("PauseObjects/area/gameStage" + (ed.stageIndex + 1).ToString() + "/spawnPoint");
				Destroy (sp.gameObject);
			}

			obj = (GameObject)Instantiate (prefabsDic[ed.prefabName], ed.enemy_pos, ed.enemy_rot);

			obj.transform.localScale = ed.enemy_scl;

			obj.transform.parent = stages [ed.stageIndex].transform;

			obj.name = ed.prefabName;
		}
	}
	// Use this for initialization
	void Awake () {
	}
}
