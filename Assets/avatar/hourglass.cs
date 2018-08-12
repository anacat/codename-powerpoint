using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class hourglass : MonoBehaviour {

	[Header("Manager")]
	[Range(0,0.99f)]
	public float fill;
	public Color color;

	[Header("Editor")]
	public List<GameObject> states;
	public List<SpriteRenderer> sprites;
	void Start () {
		
	}

	void Update () {
		for (int i = 0; i < states.Count; i++){
			bool active = (fill >= (1f /((float)states.Count))*i) && (fill < (1f /((float)states.Count))*(i+1));
			states [i].SetActive (active);
		}
		foreach (SpriteRenderer sprite in sprites)
			sprite.color = color;
	}
}
