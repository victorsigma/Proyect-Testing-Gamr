using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	}
	
	public void Play() 
	{
		GameManager.instance.LoadScene("LevelTwo");
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
