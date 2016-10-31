using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 星の画像の生成
public class s_CreateStarImages : MonoBehaviour 
{
	//public Sprite labelTexture;
	public GameObject starPrefab;

	// Use this for initialization
	void Start () 
	{
		// 一度は作成したObjectの複数生成チェックのリセット
		for (int i = 0; i < 99; i++) 
		{
			ManagerSelectStage.ItemAcquisitionRecord2 [i] = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		GameObject parent = transform.parent.gameObject;	// 親
		// 星の大きさ
		Vector2 Size = new Vector2(50.0f, 50.0f);

		// 星を表示する
		for( int i = 0; i < 3; i++) 
		{
			// ステージの番号からIDの割り出し
			int ID = int.Parse(transform.parent.name.Substring(5,2));

			// 星を描画するかどうかチェック
			if ( ManagerSelectStage.ItemAcquisitionRecord [(ID-1) * 3 + i] == true ) 
			{
				// 一度は作成したObjectの複数生成の阻止
				if (ManagerSelectStage.ItemAcquisitionRecord2 [(ID - 1) * 3 + i] == false) 
				{
					// 一度は作成した事をチェック
					ManagerSelectStage.ItemAcquisitionRecord2 [(ID - 1) * 3 + i] = true;


					GameObject obj = (GameObject)Instantiate (starPrefab, transform.parent);
					obj.GetComponent<RectTransform>().localPosition = new Vector3( -parent.GetComponent<RectTransform> ().rect.width / 4 + parent.GetComponent<RectTransform> ().rect.width / 4 * i
						, parent.GetComponent<RectTransform> ().rect.height / 2 + Size.y, 0f);
					obj.GetComponent<RectTransform> ().sizeDelta = new Vector2(	parent.GetComponent<RectTransform> ().rect.width / 4, 
						parent.GetComponent<RectTransform> ().rect.height / 4);

				}
			}
		}
	}// Update() End
}
