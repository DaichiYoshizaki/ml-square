using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class stageEdit : EditorWindow
{

	private string m_text1;
	private UnityEngine.Object m_obj1;

	private string m_text2;
	private UnityEngine.Object m_obj2;

	static public Texture trigonalTexture;
	static public Texture[] textureList;
	static public List<Texture> textureLists;

	public Texture tex;
	[MenuItem("StageEdit/edit")]
	static void editWindowOpen(){
		GetWindow<stageEdit>();
		textureList = Resources.LoadAll <Texture>("Textures");
		trigonalTexture = Resources.Load<Texture> ("Textures/1");



	}
	void OnGUI (){
		int mod = textureList.Length % 5;
		int length = (textureList.Length - mod) / 5;

		// EditorGUILayoutの使用例.
		EditorGUILayout.LabelField("createEnemy");
		for (int j = 0; j < length; j++) {
			for(int i = 0; i < 5; i++){
				if (textureList [i + j * 5]) {
					if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + j * 55.0f, 50.0f, 50.0f), textureList[i + j * 5])) {
						Debug.Log ((i + j * 5).ToString());
					}
				}
			}
		}
		for(int i = 0; i < mod; i++){
			if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + length * 55.0f, 50.0f, 50.0f), textureList[i + length * 5])) {
				Debug.Log ((i + length * 5).ToString());
			}
		}
		if (GUI.Button (new Rect (20.0f , 20.0f + (1 + length) * 55.0f, 270.0f, 50.0f), "create")) {
			Debug.Log ("Create");
		}

	}
}
