using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title : MonoBehaviour {

	public string sceneName;
	public hourglass target;
	public Camera mainCam;
	public RawImage background;
	public RawImage overlay;
	void Update () {
		background.transform.localScale = (background.transform.localScale.x + (Time.deltaTime * 0.005f)) * Vector3.one;
		target.fill += Time.deltaTime*0.040f;
		mainCam.orthographicSize -= Time.deltaTime * 0.005f;
		if (target.fill >= 1)
			overlay.gameObject.SetActive (true);
		if (target.fill >= 1.2f)
			SceneManager.LoadScene (sceneName);
	}
}
