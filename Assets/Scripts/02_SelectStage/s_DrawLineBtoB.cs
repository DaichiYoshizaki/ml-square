using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class s_DrawLineBtoB : MonoBehaviour
{
	string StartPointStage; //始点ステージ
	string NextStage;// 次ステージ


	// Use this for initialization
	void Start () 
	{
		for (int i = 1; i < 34; i++) 
		{
			// 
			StartPointStage = "Stage";
			NextStage = "Stage";

			// Stage1~9
			if (i < 10) 
			{
				int Next = i + 1;
				StartPointStage = StartPointStage + "0" + i.ToString ();

				if (i == 9) 
				{ NextStage = NextStage + Next.ToString (); } 
				else 
				{ NextStage = NextStage + "0" + Next.ToString (); }

			} 
			// Stage10~33
			else if (i >= 10) 
			{
				int Next = i + 1;
				StartPointStage = StartPointStage + i.ToString ();
				NextStage = NextStage + Next.ToString ();
			}

			GameObject StartObject = GameObject.Find (StartPointStage);
			GameObject NextObject = GameObject.Find (NextStage);

			GameObject test = new GameObject ();
			test.AddComponent <RectTransform>();
			test.transform.parent = transform;

			test.transform.name = StartPointStage + "Line";
			test.transform.position = StartObject.transform.position;
			test.AddComponent<Image> ();

			Image img = test.GetComponent<Image> ();

			img.material.mainTexture = null;

			RectTransform rt = new RectTransform ();
			rt = test.GetComponent<RectTransform> ();

			Vector2 ImageSize;
			ImageSize.x = NextObject.transform.position.x - StartObject.transform.position.x;
			ImageSize.y = NextObject.transform.position.y - StartObject.transform.position.y;
			double f = System.Math.Sqrt (ImageSize.x * ImageSize.x + ImageSize.y * ImageSize.y);
			rt.sizeDelta = new Vector2 ( (float)f, 20f );
			float rot = Mathf.Atan2(ImageSize.y, ImageSize.x);
			test.transform.rotation = Quaternion.Euler (0f, 0f, rot*(180f/3.141592f));
			Vector3 pos = new Vector3 ( ImageSize.x *0.5f, ImageSize.y * 0.5f , 0.0f );
			test.transform.position = StartObject.transform.position + pos;

		}
		/*GameObject test = new GameObject ();
		test.AddComponent <RectTransform>();
		test.transform.parent = transform;
		test.AddComponent<Image> ();
		RectTransform rt = new RectTransform ();
		rt = test.GetComponent<RectTransform> ();
		//Image img = GetComponent<Image> ();
		rt.sizeDelta = new Vector2 (rt.rect.width, 200.0f);*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position = transform.parent.position;
	
		//Debug.Log (transform.parent.name);
	}
}
