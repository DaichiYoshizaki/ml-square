using UnityEngine;
using System.Collections;
using SocialConnector;
using System.IO;

public class sheare : MonoBehaviour {

	public void OnTweet()
	{
		//StartCoroutine(Share());
		//gameManager.tweetStageIndex = gameManager.currentStageIndex;
		#if UNITY_IPHONE
		PopUp ();
		#elif UNITY_ANDROID
		PopUp ();
		#else
		gameManager.tweetStageIndex = gameManager.currentStageIndex;
		#endif
	}

	public void OnClick(){
		
	}
	void PopUp(){
		// 使う前に setlabel を呼んどく。
		DialogManager.Instance.SetLabel("Yes", "No", "Close");
		//
		// YES NO ダイアログ
		DialogManager.Instance.ShowSubmitDialog(
			"シェアしてくれてありがとう。リトライをするとステージの途中から復活できるよ！！",
			(bool result) => { if(result){ gameManager.tweetStageIndex = gameManager.currentStageIndex; } }
		);

	}
	IEnumerator Share()
	{
		// スクリーンショットをとる
		Application.CaptureScreenshot("image.png");

		// インジケーター表示
		#if UNITY_IPHONE
		Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.White);
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
