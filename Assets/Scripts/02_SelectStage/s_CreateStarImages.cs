using UnityEngine;
using System.Collections;

public class s_CreateStarImages : MonoBehaviour 
{
	public Texture2D labelTexture;
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
		for (float i = -1; i < 2; i++) 
		{
			// 座標計算
			Position.x = parent.transform.position.x + Size.x * ( i - 0.5f ) ;
			Position.y = CanvasHeight - transform.position.y - parent.GetComponent<RectTransform>().rect.height/2 - Size.y;

			// テクスチャ描画
			GUI.Label (	new Rect (Position.x, Position.y, Size.x, Size.y), labelTexture);
		}

	}
}
