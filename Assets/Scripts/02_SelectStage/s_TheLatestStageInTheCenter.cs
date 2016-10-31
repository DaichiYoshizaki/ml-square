using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// 最新ステージを中央にする。
public class s_TheLatestStageInTheCenter 
{
	public static void GoToCenterScroll()
	{
		//GameObject center = GameObject.Find ("Canvas/SelectStageScroll/Viewport/Content");
		GameObject center = GameObject.FindWithTag("content");
		Vector3 centerPos = center.GetComponent<RectTransform> ().position;
		float canvasWidth = GameObject.Find ("Canvas").GetComponent<RectTransform> ().rect.width;
		GameObject button = GameObject.Find (ManagerSelectStage.LatestCaptureStage);


		Vector3 pos = new Vector3( -1*(button.transform.localPosition.x + (canvasWidth/2)- 200), 
			centerPos.y, centerPos.z);
		if( pos.x <= 0 )
		{
			pos = new Vector3( 0, 
				centerPos.y, centerPos.z);
		}
		//Debug.Log (button.transform.localPosition.x);
		center.GetComponent<RectTransform> ().position = pos;
	}

}
