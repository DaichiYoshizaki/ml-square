using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	private Text text;
	private float countTime;

	// Use this for initialization
	void Start () {
		countTime = 15f;
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		countTime -= Time.deltaTime; //スタートしてからの秒数を格納
		text.text = countTime.ToString("00"); //小数2桁にして表示
		if(countTime <= 0){}
	}
}
