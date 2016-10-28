using UnityEngine;
using System.Collections;

/// <summary>
/// 02_SelectStageの管理用
/// </summary>
public class ManagerSelectStage : MonoBehaviour 
{
	// アイテム取得数最大 ( ステージ数 * アイテム数 )
	public const int ItemAcquisitionNumMax  = 33*3;

	// 現在選択しているステージのID
	public static int TheCurrentlySelectStageID = 0;

	// アイテム取得履歴
	public static bool[] ItemAcquisitionRecord =  new bool[ItemAcquisitionNumMax];
	public static bool[] ItemAcquisitionRecord2 =  new bool[ItemAcquisitionNumMax];

	// 最新攻略ステージ
	public static string LatestCaptureStage = "Stage01";

	// BGM
	public static int Bgm;
	// SE
	public static int Se;

	// Use this for initialization
	void Start () 
	{
		GameSaveDataOperation.LoadGameSaveDataAll ();
		SoundManager.Instance.PlayBGM (0);
		ManagerSelectStage.LatestCaptureStage = "Stage07";
	}
}
