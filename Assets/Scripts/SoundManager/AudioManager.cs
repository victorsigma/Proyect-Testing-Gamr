using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[SerializeField]
	private Sound[] musicSounds;
	[SerializeField]
	private Sound[] sfxSounnds;

	public AudioSource musicSource, sfxSource;

	[SerializeField]
	private AudioMixer audioMixer;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		if (PlayerPrefs.HasKey("musicVolume"))
		{
			SetMusicVolume();
			SetSfxVolume();
		}
	}

	public void PlayMusic(String name)
	{
		Sound s = Array.Find(musicSounds, x => x.nameTrack == name);

		if (s == null)
		{
			Debug.Log("Sound Not Found");
		}
		else
		{
			musicSource.clip = s.clip;
			musicSource.Play();
		}
	}

	public void PlaySFX(String name)
	{
		Sound s = Array.Find(sfxSounnds, x => x.nameTrack == name);

		if (s == null)
		{
			Debug.Log("Sound Not Found");
		}
		else
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}

	public void ToggleMusic()
	{
		musicSource.mute = !musicSource.mute;
	}

	public void ToggleSfx()
	{
		sfxSource.mute = !sfxSource.mute;
	}

	public void MusicVolume(float volume)
	{
		musicSource.volume = volume;
	}

	public void SfxVolume(float volume)
	{
		sfxSource.volume = volume;
	}

	private void SetMusicVolume()
	{
		float volume = PlayerPrefs.GetFloat("musicVolume");
		audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("musicVolume", volume);
	}

	private void SetSfxVolume()
	{
		float volume = PlayerPrefs.GetFloat("sfxVolume");
		audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("sfxVolume", volume);
	}
}
