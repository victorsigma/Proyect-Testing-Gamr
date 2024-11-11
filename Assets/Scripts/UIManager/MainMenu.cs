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
		GameManager.instance.LoadScene("MapTraslation");
	}
	
	public void Custom() 
	{
		eventSystem.SetSelectedGameObject(customizationFirstButton);
		customizationMenu.SetActive(true);
		customizationMenu.GetComponent<CustomizationMenu>().Reload();
		gameObject.SetActive(false);
	}
	
	public void Exit()
	{
		//UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
