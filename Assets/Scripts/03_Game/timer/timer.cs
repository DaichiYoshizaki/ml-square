using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	public GameObject timerText;
	private Text text;
	private float countTime;
	static private bool isTimeStop;

	public float CountTime{
		get{ return countTime;}
		set{ countTime = value;}
	}

	static public bool IsTimeStop{
		get{ return isTimeStop; }
	}

	// Use this for initialization
	void Start () {
		countTime = 15f;
		text = timerText.GetComponent<Text> ();
		isTimeStop = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isTimeStop) {
			countTime -= Time.deltaTime; //スタートしてからの秒数を格納
		}
		text.text = countTime.ToString("00"); //小数2桁にして表示
		if (countTime < 0f) {
			gameClearManager.stageClear ();
			isTimeStop = true;
			countTime = 15;
		}
	}
	static public void StartTimer(){
		isTimeStop = false;
	}
	static public void StopTimer(){
		isTimeStop = true;
	}
}
