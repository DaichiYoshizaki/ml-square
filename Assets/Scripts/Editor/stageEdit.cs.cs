using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class stageEdit : EditorWindow
{

	static stageEdit editWindow;

	static public Texture noneImage;
	static public Texture[] textureList;
	static public GameObject[] prefabs;

	static public GameObject currentSerchObject;

	static private GameObject[] stages = {null, null, null};

	static private int stageIndex;

	public enum stageNum{
		stage1,
		stage2,
		stage3
	};

	private GUISkin buttonGuiSkin;  //ボタンのスタイル設定用 

	public stageNum numtoint;

	public bool isFirstTime = true;

	public Texture tex;
	[MenuItem("StageEdit/edit")]
	static void editWindowOpen(){
		if (editWindow == null) {
			editWindow = CreateInstance<stageEdit> ();
		}

		editWindow.ShowUtility ();
	}
	void OnGUI (){
		if (isFirstTime) {
			isFirstTime = false;
			prefabs = Resources.LoadAll <GameObject>("Prefabs");

			Array.Resize(ref textureList, prefabs.Length);

			stageNum stageNum = stageNum.stage1;

			for (int i = 0; i < prefabs.Length; i++) {

				if (prefabs [i].GetComponent<SpriteRenderer> ()) {
					textureList [i] = prefabs [i].GetComponent<SpriteRenderer> ().sprite.texture;
				}
				else if(prefabs [i].GetComponentInChildren<SpriteRenderer> ()){
					textureList [i] = prefabs [i].GetComponentInChildren<SpriteRenderer> ().sprite.texture;
				}
				else{
					textureList [i] = null;
				}

			}

			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) {
				if (obj.name == "gameStage1") {
					stages [0] = obj;
				}
				if (obj.name == "gameStage2") {
					stages [1] = obj;
				}
				if (obj.name == "gameStage3") {
					stages [2] = obj;
				}
			}

		}
		if (EditorApplication.isPlaying) {
			isFirstTime = true;
		}
		if (!EditorApplication.isPlaying) {
			int mod = textureList.Length % 5;
			int length = (textureList.Length - mod) / 5;

			GameObject obj;
			GameObject[] sel = { null };

			// EditorGUILayoutの使用例.
			EditorGUILayout.LabelField ("createEnemy");
			for (int j = 0; j < length; j++) {
				for (int i = 0; i < 5; i++) {
					if (textureList [i + j * 5]) {
						if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + j * 55.0f, 50.0f, 50.0f), textureList [i + j * 5])) {
							Debug.Log (prefabs [i + j * 5].name);
							obj = GameObject.Instantiate (prefabs [i + j * 5].gameObject);
							obj.transform.parent = stages [stageIndex].transform;
							obj.transform.position = stages [stageIndex].transform.position;
							obj.name = prefabs [i + j * 5].name;
							sel [0] = obj;
							Selection.objects = sel;

							currentSerchObject = obj;

							SceneView.onSceneGUIDelegate += sceneView;

							Undo.RegisterCreatedObjectUndo (obj, "object create");
						}
					} else {
					
						if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + j * 55.0f, 50.0f, 50.0f), prefabs [i + j * 5].name)) {
							Debug.Log (prefabs [i + j * 5].name);
							obj = GameObject.Instantiate (prefabs [i + j * 5].gameObject);
							obj.transform.parent = stages [stageIndex].transform;
							obj.transform.position = stages [stageIndex].transform.position;
							obj.name = prefabs [i + j * 5].name;
							sel [0] = obj;
							Selection.objects = sel;

							currentSerchObject = obj;

							SceneView.onSceneGUIDelegate += sceneView;

							Undo.RegisterCreatedObjectUndo (obj, "object create");
						}
					}
				}
			}
			for (int i = 0; i < mod; i++) {
				if (textureList [i + length * 5]) {
					if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + length * 55.0f, 50.0f, 50.0f), textureList [i + length * 5])) {
						Debug.Log (prefabs [i + length * 5].name);
						obj = GameObject.Instantiate (prefabs [i + length * 5].gameObject);
						obj.transform.parent = stages [stageIndex].transform;
						obj.transform.position = stages [stageIndex].transform.position;
						obj.name = prefabs [i + length * 5].name;
						sel [0] = obj;
						Selection.objects = sel;

						currentSerchObject = obj;

						SceneView.onSceneGUIDelegate += sceneView;

						Undo.RegisterCreatedObjectUndo (obj, "object create");
					}
				} else {
					if (GUI.Button (new Rect (20.0f + i * 55.0f, 20.0f + length * 55.0f, 50.0f, 50.0f), prefabs [i + length * 5].name)) {
						Debug.Log (prefabs [i + length * 5].name);
						obj = GameObject.Instantiate (prefabs [i + length * 5].gameObject);
						obj.transform.parent = stages [stageIndex].transform;
						obj.transform.position = stages [stageIndex].transform.position;
						sel [0] = obj;
						Selection.objects = sel;

						currentSerchObject = obj;

						SceneView.onSceneGUIDelegate += sceneView;
						SceneView.onSceneGUIDelegate -= sceneView;

						Undo.RegisterCreatedObjectUndo (obj, "object create");
					}
				}
			}

			numtoint = (stageNum)EditorGUI.EnumPopup (new Rect (20.0f, 20.0f + (1 + length) * 55.0f, 270.0f, 50.0f), "create on stage index", numtoint);
			stageIndex = (int)numtoint;

			if (GUI.Button (new Rect (20.0f, 20.0f + (2 + length) * 55.0f, 270.0f, 50.0f), "CSV Export")) {
				Debug.Log ("CSV Export");

			}
		}
	}
	void sceneView(SceneView sceneView){
		sceneView.LookAt(new Vector3(currentSerchObject.transform.position.x, currentSerchObject.transform.position.y, currentSerchObject.transform.position.z));
		SceneView.onSceneGUIDelegate -= this.sceneView;
	}
}
