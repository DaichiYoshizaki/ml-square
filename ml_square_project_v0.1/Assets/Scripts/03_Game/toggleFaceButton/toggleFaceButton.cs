using UnityEngine;
using System.Collections;

public class toggleFaceButton : MonoBehaviour {
	
	public GameObject player;
	private playerMover playerMover;

	// Use this for initialization
	void Start () {
		playerMover = player.GetComponent<playerMover> ();
	}
	
	// Update is called once per frame
	public void OnClick () {
		playerMover.changeFace ();
	}
}
