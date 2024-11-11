using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
	[SerializeField]
	private AudioMixer audioMixer;
	
	[SerializeField]
	private Slider musicSlider;
	
	[SerializeField]
	private Slider sfxSlider;
	
	[SerializeField]
	private TextMeshProUGUI musicValue;
	
	[SerializeField]
	private TextMeshProUGUI sfxValue;
	
	void Start() 
	{
		if(PlayerPrefs.HasKey("musicVolume")) 
		{
			LoadVolume();
		} else 
		{
			SetMusicVolume();
			SetSfxVolume();
		}
	}
	
	public void SetMusicVolume() 
	{
		float volume = musicSlider.value;
		audioMixer.SetFloat("musicVolume", Mathf.Log10(volume)*20);
		PlayerPrefs.SetFloat("musicVolume", volume);
		
		float percentage = (musicSlider.value - musicSlider.minValue)/(musicSlider.minValue - musicSlider.maxValue)*100*-1;
		musicValue.text = Math.Round(percentage).ToString()+"%";
	}
	
	public void SetSfxVolume() 
	{
		float volume = sfxSlider.value;
		audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume)*20);
		PlayerPrefs.SetFloat("sfxVolume", volume);
		
		float percentage = (sfxSlider.value - sfxSlider.minValue)/(sfxSlider.minValue - sfxSlider.maxValue)*100*-1;
		sfxValue.text = Math.Round(percentage).ToString()+"%";
	}
	
	private void LoadVolume() 
	{
		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
		
		float percentageMusic = (musicSlider.value - musicSlider.minValue)/(musicSlider.minValue - musicSlider.maxValue);
		musicValue.text = Math.Round(percentageMusic).ToString()+"%";
		float percentageSfx = (sfxSlider.value - sfxSlider.minValue)/(sfxSlider.minValue - sfxSlider.maxValue);
		sfxValue.text = Math.Round(percentageSfx).ToString()+"%";
		
		SetMusicVolume();
		SetSfxVolume();
	}
}
