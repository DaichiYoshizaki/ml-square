using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class enemyData : ScriptableObject {
		public Texture2D enemyTexture;
		public string enemyName;
		public string enemyIntro;
	[MenuItem("CreateEnemyData/Create")]
	static public void EnemyDataCreate(){
		for (int i = 0; i < 12; i++) {
			enemyData ed = ScriptableObject.CreateInstance<enemyData> ();
			string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/enemyData/" + i + ".asset");
		
			AssetDatabase.CreateAsset(ed, path);
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();
			Selection.activeObject = ed;
		}
	}
}