using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class createButtonCreateEnemy : MonoBehaviour {
	private List<GameObject> prefabs;
	private List<Texture> textures;
	// Use this for initialization
	void Start () {
		GameObject content = GameObject.Find ("ScrollBarCanvas/Scroll View/Viewport/Content");
		prefabs.AddRange(Resources.LoadAll <GameObject>("Prefabs"));
		foreach (var prefab in prefabs) {
			if (prefab.GetComponent<SpriteRenderer> ()) {
				textures.Add (prefab.GetComponent<SpriteRenderer> ().sprite.texture);
			}
			else if(prefab.GetComponentInChildren<SpriteRenderer> ()){
				textures.Add (prefab.GetComponentInChildren<SpriteRenderer> ().sprite.texture);
			}
			else{
				textures = null;
			}
		}
		foreach (var texture in textures.Select((v, i) => new{v, i})) {
			
		}
	}
	void FixedUpdate(){
	}
}
