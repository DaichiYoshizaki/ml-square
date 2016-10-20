using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	public GameObject timerText;
	private Text text;
	private float countTime;

	public float CountTime{
		get{ return countTime;}
		set{ countTime = value;}
	}

	// Use this for initialization
	void Start () {
		countTime = 15f;
		text = timerText.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countTime -= Time.deltaTime; //スタートしてからの秒数を格納
		text.text = countTime.ToString("00"); //小数2桁にして表示
		if(countTime < 0) countTime = 15;
	}
}
