using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject customizationMenu;
	
	[SerializeField]
	private GameObject customizationFirstButton;
	
	[SerializeField]
	private GameObject scoreMenu;
	
	
	[SerializeField]
	private GameObject scrollScore;
	
	EventSystem eventSystem;
	void Start() 
	{
		eventSystem = EventSystem.current;
		AudioManager.instance.PlayMusic("MainMenu");
		//audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("Music"));
		//audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("SFX"));
	}
	
	public void Play() 
	{
		if(PlayerPrefs.GetInt("tutorial") == 0) 
		{
			PlayerPrefs.SetInt("tutorial", 1);
			GameManager.instance.LoadScene("Tutorial");
		} else 
		{
			GameManager.instance.LoadScene("MapTraslation");
		}
		
	}
	
	public void Custom() 
	{
		eventSystem.SetSelectedGameObject(customizationFirstButton);
		customizationMenu.SetActive(true);
		customizationMenu.GetComponent<CustomizationMenu>().Reload();
		gameObject.SetActive(false);
	}
	
	public void Score() 
	{
		eventSystem.SetSelectedGameObject(scrollScore);
		scoreMenu.SetActive(true);
		gameObject.SetActive(false);
	}
	
	public void Exit()
	{
		//UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
