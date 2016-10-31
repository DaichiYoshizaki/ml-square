using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fade : MonoBehaviour {
	private Color fadeColor = Color.black; // フェード時の色。無難に黒に設定
	private float alpha = 1; // アルファ値
	private float speed; // アルファ値の変化量
	private float fadeInTime = 2; // フェードイン開始から終了までに要する時間
	private float fadeOutTime = 2; // フェードアウト開始から終了までに要する時間
	private bool fadeFlag = true; // フェード処理実行中判定フラグ
	private bool fadeInFlag = true; // 処理がフェードインかフェードアウトかの管理フラグ
	private string nextScene; // 移行先シーン名保存用
	static public Fade instance; // FadeOut関数呼び出し用

	// Use this for initialization
	void Start( ) {
		// シーン切り替えで破棄されないように設定
		// 既にFadeが存在している場合、多重に存在してしまわないように自身を破棄
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		}
		else {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update( ) {
	}

	void OnGUI( ) {
		if(fadeFlag) {
			// アルファ値変化量計算
			if(fadeInFlag) {
				// 処理がfadeInならアルファ値を0にするため変化量をマイナスに
				speed = -Time.deltaTime / fadeInTime;
			}
			else {
				speed = Time.deltaTime / fadeOutTime;
			}

			// アルファ値更新
			alpha += speed;

			// アルファ値が0or1になったらフェード処理終了
			if(fadeInFlag && alpha < 0) {
				alpha = 0;
				// フェードフラグOFF。FadeOut関数が呼ばれるまで待機
				fadeFlag = false;
			}
			else if(!fadeInFlag && alpha > 1) {
				alpha = 1;
				// 処理をFadeInに切り替え。LoadSceneが終わったらFadeInが開始する・・・はず。LoadScene重かったら終わる前にFadeIn開始されたりするのかな
				fadeInFlag = true;
				SceneManager.LoadScene(nextScene);
			}

			// アルファ値反映
			fadeColor.a = alpha;
			GUI.color = fadeColor;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
		}
	}

	// フェード呼び出し関数。次に呼び出すべきシーン名とフェード時間を指定する。
	public bool FadeOut(string getScene, float outTime, float inTime) {
		if (fadeFlag) {
			// Fade処理中であればreturn
			// FadeIn処理中は次シーンへ移行しない場合は if(fadeFlag)
			// FadeIn処理中でも次シーンへ移行していいのであれば　if(fadeFlag && !fadeInFlag)
			return false;
		}
		else
		{
			// FadeOut設定
			fadeInFlag = false;
			fadeFlag = true;
			nextScene = getScene;

			// フェード時間が0以下の場合は強制で0.1fに
			// 割り算に使用するので0だとヤバい。マイナスだと足し算という名の引き算になって不等号条件式が満たされずフェードが一生終わらない
			if(outTime <= 0) {
				outTime = 0.1f;
			}
			fadeOutTime = outTime;

			if(inTime <= 0) {
				inTime = 0.1f;
			}
			fadeInTime = inTime;
			return true;
		}
	}
}
