using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyListButton : MonoBehaviour {
	public enemyData ed;
	public int id;
	public int spawnAreaIndex = 0;
	static int a = 20;
	private bool beAbleTap = true;

	public void Start(){
		ed = Resources.Load ("enemyData/" + id) as enemyData;
		transform.FindChild("EnemyImage").GetComponent<Image> ().sprite =  Sprite.Create(ed.enemyTexture, new Rect(0f, 0f, ed.enemyTexture.width, ed.enemyTexture.height), Vector2.zero);

		enemyData trigonal = Resources.Load ("enemyData/0") as enemyData;
		GameObject.Find ("EnemyListImage").GetComponent<Image> ().sprite = Sprite.Create (trigonal.enemyTexture, new Rect (0f, 0f, trigonal.enemyTexture.width, trigonal.enemyTexture.height), Vector2.zero);
		GameObject.Find ("enemyInfoPanel/enemyInfoNamePanel/enemyInfoNamePanelText").GetComponent<Text> ().text = trigonal.enemyName;
		GameObject.Find ("enemyInfoPanel/enemyInfoIntroPanel/enemyInfoIntroPanelText").GetComponent<Text> ().text = trigonal.enemyIntro;


		if (a < spawnAreaIndex) {
			beAbleTap = false;
			GetComponent<Image> ().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
			transform.FindChild("EnemyImage").GetComponent<Image> ().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
		}
	}
	public void Onclick(int id){
		if (beAbleTap){
			Debug.Log (id);
			GameObject.Find ("EnemyListImage").GetComponent<Image> ().sprite = Sprite.Create (ed.enemyTexture, new Rect (0f, 0f, ed.enemyTexture.width, ed.enemyTexture.height), Vector2.zero);
			GameObject.Find ("enemyInfoPanel/enemyInfoNamePanel/enemyInfoNamePanelText").GetComponent<Text> ().text = ed.enemyName;
			GameObject.Find ("enemyInfoPanel/enemyInfoIntroPanel/enemyInfoIntroPanelText").GetComponent<Text> ().text = ed.enemyIntro;
		}
	}
}
