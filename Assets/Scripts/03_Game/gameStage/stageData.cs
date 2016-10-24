using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class stageData : ScriptableObject {
	//敵が出現する場所とID
	public List<EnemyDetail> entryEnemy;
}

[System.Serializable]
public class EnemyDetail{
	public string prefabName;
	public int stageIndex;
	public Vector3 enemy_pos;
	public Quaternion enemy_rot;
	public Vector3 enemy_scl;
}
