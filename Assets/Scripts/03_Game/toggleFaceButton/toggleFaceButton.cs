using UnityEngine;
using System.Collections;

public class toggleFaceButton : MonoBehaviour {
	
	private GameObject player;
	private playerMover playerMover;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("gamePlayer");
		playerMover = player.GetComponent<playerMover> ();
	}
	
	// Update is called once per frame
	public void OnClick () {
		playerMover.changeFace ();
		SoundManager.Instance.PlaySE(0);
	}
}
