using UnityEngine;
using System.Collections;

public class playerJump : MonoBehaviour {

	private bool isAbleToJump = false;
	public bool IsAbleToJump{
		get{ return isAbleToJump; }
		set{ isAbleToJump = value; }
	}
	private Rigidbody2D rb2d;
	private float playerJumpAbletime = 0f;
	// Use this for initialization
	void Start () {
		isAbleToJump = false;
		rb2d = GetComponent<Rigidbody2D> ();
		playerJumpAbletime = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerJumpAbletime += 0.1f;
		if (isAbleToJump && playerJumpAbletime > 2f){
			rb2d.AddForce (Vector2.up * 500f);
			SoundManager.Instance.PlaySE (1);
			isAbleToJump = false;
		}
	}
}
