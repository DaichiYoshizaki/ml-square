using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fade : MonoBehaviour {
	private Color fadeColor = Color.black;
	private float alpha = 1;
	private float speed;
	private float fadeInTime = 2;
	private float fadeOutTime;
	private bool fadeFlag = true;
	private bool fadeInFlag = true;
	private string nextScene;
	static public Fade instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		}
		else {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	}

	void OnGUI( ) {
		if (fadeFlag) {
			fadeColor.a = alpha;
			GUI.color = fadeColor;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

			if(fadeInFlag) {
				speed = -Time.deltaTime / fadeInTime;
				if (alpha < 0)
				{
					// この後alpha += speedされるので-speedを入れて0になるように調整。
					alpha = -speed;
					// フェードフラグOFF。FadeOutが呼ばれるまで待機。
					fadeFlag = false;
				}
			}
			if(!fadeInFlag) {
				speed = Time.deltaTime / fadeOutTime;
				if (alpha > 1)
				{
					// この後alpha += speedされるので-speedを入れて1になるように調整。
					alpha = 1 - speed;
					// 処理をFadeInに切り替え。LoadSceneが終わったらFadeInが開始する。
					fadeInFlag = true;
					if (nextScene == SceneManager.GetActiveScene().name) {
						SceneManager.LoadScene("06_Loading");
					}
					SceneManager.LoadScene(nextScene);
				}
			}
			// アルファ値更新。更新いたアルファ値で一度はDrawTextureを呼ぶために最後に配置
			alpha += speed;
		}
	}

	// フェード呼び出し関数。次に呼び出すべきシーン名とフェード時間を指定する。
	public bool FadeOut(string getScene, float outTime, float inTime) {
		if (fadeFlag) {
			// Fade処理中であればreturn。
			// FadeIn処理中は次シーンへ移行しない場合は if(fadeFlag)。
			// FadeIn処理中でも次シーンへ移行していいのであれば　if(fadeFlag && !fadeInFlag)。
			return false;
		}
		else
		{
			// FadeOut設定
			fadeInFlag = false;
			fadeFlag = true;
			nextScene = getScene;

			// フェード時間が0以下の場合は強制で0.1fに。
			// 割り算に使用するので0だとヤバい。マイナスだとアルファ値に足す数値の符号が逆転してフェードが一生終わらない。
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
