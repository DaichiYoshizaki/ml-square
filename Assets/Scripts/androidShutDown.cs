using UnityEngine;
using System.Collections;

public class androidShutDown : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		// プラットフォームがアンドロイドかチェック
		if (Application.platform == RuntimePlatform.Android)
		{
			// エスケープキー取得
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				// 使う前に setlabel を呼んどく。
				DialogManager.Instance.SetLabel("Yes", "No", "Close");
				//
				// YES NO ダイアログ
				DialogManager.Instance.ShowSelectDialog(
					"アプリを終了しますか",
					(bool result) => { 				// アプリケーション終了
						if(result){
			
							Application.Quit();
						return; }
					}
				);
			}
		}	
	}
}
