using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fade : MonoBehaviour {
	float red, green, blue;
	float alpha = 1;
	float speed = 0.05f;
	static bool fadeFlag = true;
	static bool fadeInFlag = true;
	static string nextScene;

	// Use this for initialization
	void Start () {
		red = GetComponent<Image>().color.r;
		green = GetComponent<Image>().color.g;
		blue = GetComponent<Image>().color.b;
	}

	// Update is called once per frame
	void Update () {
		if (fadeFlag) {
			if(fadeInFlag) {
				GetComponent<Image>().color = new Color(red, green, blue, alpha);

				if (alpha < 0)
				{
					alpha = 0;
					fadeFlag = false;
				}

				alpha -= speed;
			}
			if(!fadeInFlag) {
				GetComponent<Image>().color = new Color(red, green, blue, alpha);

				if (alpha > 1)
				{
					alpha = 1;
					fadeInFlag = true;
					SceneManager.LoadScene(nextScene);
				}

				alpha += speed;
			}
		}
	}

	bool FadeOut(string getScene) {
		if (fadeFlag) {
			return false;
		}
		else
		{
			fadeInFlag = false;
			fadeFlag = true;
			nextScene = getScene;
			return true;
		}
	}
}
