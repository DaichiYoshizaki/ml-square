using UnityEngine;
using System.Collections;
using SocialConnector;
using System.IO;

public class sheare : MonoBehaviour {

	public void OnTweet()
	{
		StartCoroutine(Share());
	}

	public void OnClick(){
		OnTweet ();
	}

	IEnumerator Share()
	{
		// スクリーンショットをとる
		Application.CaptureScreenshot("image.png");

		// インジケーター表示
		#if UNITY_IPHONE
		Handheld.SetActivityIndicatorStyle(iOSActivityIndicatorStyle.White);
		#elif UNITY_ANDROID
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		#endif
		Handheld.StartActivityIndicator();

		// スクリーンショットが保存されるまで待機
		long filesize = 0;

		string filePath = Path.Combine(Application.persistentDataPath, "image.png");

		while (filesize == 0) 
		{
			yield return null;

			//ファイルのサイズを取得
			System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
			if (fi != null) {
				filesize = fi.Length;
			}
		}
			
		// インジケーター非表示
		Handheld.StopActivityIndicator();

		// shareのテキスト内容
		string tweet = "" + "100点を記録しました!!";

		// 共有
		SocialConnector.SocialConnector.Share(tweet, "", filePath);
	}

}
