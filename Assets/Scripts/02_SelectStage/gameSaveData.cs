using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[InitializeOnLoad]
public class gameSaveData : ScriptableObject
{
	// 最新攻略ステージ
	public string LatestCaptureStage;

	// アイテム取得履歴
	public bool[] ItemAcquisitionRecord =  new bool[ManagerSelectStage.ItemAcquisitionNumMax];
	// BGM
	public int Bgm;
	// SE
	public int Se;
}
#if UNITY_EDITOR
public static class CreateSaveData
{
	public static  void Create(string stageName, bool[] ItemRcord, int BGM, int SE)
	{
		gameSaveData gsd = ScriptableObject.CreateInstance<gameSaveData> ();

		gsd.LatestCaptureStage = stageName;

		gsd.ItemAcquisitionRecord = ItemRcord;

		gsd.Bgm = BGM;

		gsd.Se = SE;

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/SaveData.asset");

		AssetDatabase.CreateAsset(gsd, path);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = gsd;

	}
}
#endif
// ゲームセーブデータの操作
public static class GameSaveDataOperation
{
	
	public static gameSaveData SaveData;

	// 全てのデータを取得
	public static void LoadGameSaveDataAll()
	{
		SaveData = Resources.Load<gameSaveData> ("SaveData");

		// 最新攻略ステージ
		ManagerSelectStage.LatestCaptureStage = SaveData.LatestCaptureStage;

		// アイテム取得履歴
		for (int i = 0; i < 99; i++) 
		{
			ManagerSelectStage.ItemAcquisitionRecord[i] = SaveData.ItemAcquisitionRecord[i];
		}

		// BGM
		ManagerSelectStage.Bgm = SaveData.Bgm;
		// SE
		ManagerSelectStage.Se = SaveData.Se;
	}
	// 全てのセーブデータを更新
	public static void SaveGameSaveDataAll()
	{
		SaveData = Resources.Load<gameSaveData> ("SaveData");

		// 最新攻略ステージ
		SaveData.LatestCaptureStage = ManagerSelectStage.LatestCaptureStage;

		// アイテム取得履歴
		SaveData.ItemAcquisitionRecord = ManagerSelectStage.ItemAcquisitionRecord;

		// BGM
		SaveData.Bgm = ManagerSelectStage.Bgm;
		// SE
		SaveData.Se = ManagerSelectStage.Se;
	}

	// 
}

