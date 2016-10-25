using UnityEngine;
using System.Collections;

/// <summary>
/// 02_SelectStageの管理用
/// </summary>
public class ManagerSelectStage : MonoBehaviour 
{
	// アイテム取得数最大 ( ステージ数 * アイテム数 )
	const int ItemAcquisitionNumMax  = 33*3;

	// 現在選択しているステージのID
	public static int TheCurrentlySelectStageID = 0;
	// アイテム取得履歴
	public static bool[] ItemAcquisitionRecord =  new bool[ItemAcquisitionNumMax];

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
