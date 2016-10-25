using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class saveStageData : MonoBehaviour {
	public static void CreateAsset(stageData editItem, int areaIndex)
	{   
		stageData item = ScriptableObject.CreateInstance<stageData>();

		item.entryEnemy = new List<EnemyDetail> ();

		item.entryEnemy = editItem.entryEnemy;

		//アセットを保存するパス
		string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/stageData/" + "stage" + areaIndex + ".asset");

		AssetDatabase.CreateAsset(item, path);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = item;
	}
}
