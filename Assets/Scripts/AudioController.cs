using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	public AudioClip levelUp;
	public AudioClip levelDown;

	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayLevelUp()
	{
		_audioSource.PlayOneShot(levelUp);
	}

	public void PlayLevelDown()
	{
		_audioSource.PlayOneShot(levelDown);
	}
}
