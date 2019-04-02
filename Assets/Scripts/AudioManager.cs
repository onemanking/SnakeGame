using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager _instance = null;
	public static AudioManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType (typeof (AudioManager)) as AudioManager;
				if (_instance == null)
				{
					GameObject go = new GameObject ();
					_instance = go.AddComponent<AudioManager> ();
					go.name = "AudioManager";
				}
			}

			return _instance;
		}
	}

	public AudioSource sfxAudioSource;

	public AudioSource bgmAudioSource;

	public AudioSource uiAudioSource;

	[Header ("BGM")]
	[SerializeField]
	private AudioClip mainMenuBGM;
	[SerializeField]
	private AudioClip gameOverBGM;

	[Header ("SNAKE EFFECT")]
	[SerializeField]
	private AudioClip foodSound;
	[SerializeField]
	private AudioClip hitSound;

	[Header ("UI")]
	[SerializeField]
	private AudioClip clickSFX;

	public void SetSoundMute (bool isMute)
	{
		sfxAudioSource.mute = isMute;
		bgmAudioSource.mute = isMute;
		uiAudioSource.mute = isMute;
	}

	public void PlayBGM (string clipName, bool isLoop = true)
	{
		if (isLoop)
		{
			bgmAudioSource.loop = true;

		}
		else
		{
			bgmAudioSource.loop = false;
		}
		bgmAudioSource.clip = GetClipFromName (clipName);
		bgmAudioSource.Play ();
	}

	public void PlaySoundEffect (string clipName, bool isLoop = false, float volume = 1.0f)
	{
		sfxAudioSource.volume = volume;

		if (isLoop)
		{
			sfxAudioSource.loop = true;
			sfxAudioSource.clip = GetClipFromName (clipName);
			sfxAudioSource.Play ();
		}
		else
		{
			sfxAudioSource.loop = false;
			sfxAudioSource.clip = null;
			sfxAudioSource.PlayOneShot (GetClipFromName (clipName));
		}
	}

	public void PlayUISound (string clipName = null)
	{
		if (clipName == null)
		{
			uiAudioSource.PlayOneShot (GetClipFromName ("Click"));
		}
		else
		{
			uiAudioSource.PlayOneShot (GetClipFromName (clipName));
		}
	}

	AudioClip GetClipFromName (string name)
	{
		if (name.StartsWith ("MainMenu"))
		{
			return mainMenuBGM;
		}
		else if (name.StartsWith ("GameOver"))
		{
			return gameOverBGM;
		}
		else if (name.StartsWith ("Click"))
		{
			return clickSFX;
		}
		else if (name.StartsWith ("Reward"))
		{
			return foodSound;
		}
		else if (name.StartsWith ("Hit"))
		{
			return hitSound;
		}
		else
		{
			Debug.LogWarning ("NO AUDIO CHIP FOUND");
			return null;
		}
	}

	public void StopBGM ()
	{
		bgmAudioSource.Stop ();
	}

	public void PausePlay ()
	{
		sfxAudioSource.Pause ();
	}

	public void CountinuePlay ()
	{
		sfxAudioSource.Play ();
	}
}