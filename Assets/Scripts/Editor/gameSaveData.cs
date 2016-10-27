using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class gameSaveData : ScriptableObject
{
	public string stage;

	static public bool[] ItemAcquisitionRecord =  new bool[99];

	public int bgm;

	public int se;
}
public static class CreateSaveData{
	[MenuItem("Assets/SaveData/Create")]	
	public static  void Create(){
		gameSaveData gsd = ScriptableObject.CreateInstance<gameSaveData> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/SaveData.asset");

		AssetDatabase.CreateAsset(gsd, path);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = gsd;

	}
}
