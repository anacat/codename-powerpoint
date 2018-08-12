using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusicController : MonoBehaviour 
{
	public AudioClip loop1;
	public AudioClip loop2;
	public AudioClip loop3;

	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();

		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += CheckCurrentLevel;
	}

	private void CheckCurrentLevel(Scene previousScene, Scene newScene)
	{	
		_audioSource = GetComponent<AudioSource>();

		if(Int32.Parse(newScene.name) == 1)
		{
			StartCoroutine(SeamlessTransition(loop1));
		}
		else if(Int32.Parse(newScene.name) == 11)
		{
			StartCoroutine(SeamlessTransition(loop2));
		}
		else if(Int32.Parse(newScene.name) == 21)
		{
			StartCoroutine(SeamlessTransition(loop3));
		}
	}

	private IEnumerator SeamlessTransition(AudioClip newClip)
	{
		yield return new WaitUntil(() =>  _audioSource.clip.length - _audioSource.time <= 0.005f);

		_audioSource.clip = newClip;
		_audioSource.Play();
	}

	private void OnDestroy()
	{
		SceneManager.activeSceneChanged -= CheckCurrentLevel;		
	}
}
