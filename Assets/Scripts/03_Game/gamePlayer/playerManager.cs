using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {
	private bool isDead = false;
	// Use this for initialization
	void Start () {
		isDead = false;
	}
	public bool IsDead{
		set{ isDead = value;}
		get{ return isDead;}
	}
}
