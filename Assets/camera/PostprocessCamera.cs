using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostprocessCamera : MonoBehaviour {

	private Material material;
	//public float glitch;
	public Color a, b;

	void Awake (){
		material = new Material( Shader.Find("Camera/Recolor") );
	}
		
	void OnRenderImage (RenderTexture source, RenderTexture destination){
		//material.SetFloat ("_Glitch", glitch);
		material.SetColor("_ColorA", a);
		material.SetColor ("_ColorB", b);
		Graphics.Blit (source, destination, material);
	}
}
