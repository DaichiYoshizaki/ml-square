using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class s_CreateStarImages : MonoBehaviour 
{
	//public Sprite labelTexture;
	public GameObject starPrefab;
	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		OnGUI ();
	}

	void OnGUI ()
	{
		GameObject canvas = GameObject.Find ("Canvas");		// Canvas
		GameObject parent = transform.parent.gameObject;	// 親

		Vector2 Size = new Vector2(50.0f, 50.0f);

		Vector2 Position;
		// Canvasの高さ
		float CanvasHeight = canvas.GetComponent<RectTransform>().rect.height ;

		// 星を表示する
		for( int i = 0; i < 3; i++) 
		{
			int ID = int.Parse(transform.parent.name.Substring(5,2));

			if ( ManagerSelectStage.ItemAcquisitionRecord [(ID-1) * 3 + i] == true ) 
			{
				if (ManagerSelectStage.ItemAcquisitionRecord2 [(ID - 1) * 3 + i] == false) 
				{
					ManagerSelectStage.ItemAcquisitionRecord2 [(ID - 1) * 3 + i] = true;
					// 座標計算
					//Position.x = parent.transform.position.x + Size.x * ((float)i - 1.5f);
					//Position.y = CanvasHeight - transform.position.y - parent.GetComponent<RectTransform> ().rect.height / 2 - Size.y;

					// テクスチャ描画
					//GUI.Label (new Rect (Position.x, Position.y, Size.x, Size.y), labelTexture);


//					GameObject test = new GameObject();
//					test.AddComponent<RectTransform> ();
//					rt = test.GetComponent<RectTransform> ();
//
//					test.AddComponent<Image> ();
//					Sprite sprite = test.GetComponent<Image> ().sprite;
//
//					sprite = labelTexture;

//					test.transform.parent = transform.parent;
//					test.transform.position = new Vector3 ( transform.parent.transform.position.x + 50.0f * ((float)i - 1.5f),
//															transform.parent.transform.position.y + parent.GetComponent<RectTransform> ().rect.height / 2 + Size.y,
//															0.0f);
				
//					rt.sizeDelta = new Vector2 (50.0f, 50.0f);
//
//					sprite = null;

					GameObject obj = (GameObject)Instantiate (starPrefab, transform.parent);
					obj.GetComponent<RectTransform>().localPosition = new Vector3( -parent.GetComponent<RectTransform> ().rect.width / 4 + parent.GetComponent<RectTransform> ().rect.width / 4 * i
																	, parent.GetComponent<RectTransform> ().rect.height / 2 + Size.y, 0f);
					obj.GetComponent<RectTransform> ().sizeDelta = new Vector2(	parent.GetComponent<RectTransform> ().rect.width / 4, 
																				parent.GetComponent<RectTransform> ().rect.height / 4);
					//Debug.Log (parent.transform);

				}
			}
		}

	}
}
