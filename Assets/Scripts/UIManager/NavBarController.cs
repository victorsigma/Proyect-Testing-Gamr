using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavBarController : MonoBehaviour
{
	[SerializeField]
	private GameObject characterSettigs;
	[SerializeField]
	private GameObject audioSettigs;
	
	[SerializeField]
	private GameObject characterButton;
	[SerializeField]
	private GameObject audioButton;

	EventSystem eventSystem;
	void Start() 
	{
		eventSystem = EventSystem.current;
	}


	public void EnableCharacterSettigs() 
	{
		characterSettigs.SetActive(true);
		audioSettigs.SetActive(false);
		eventSystem.SetSelectedGameObject(audioButton);
	}
	
	public void EnableAudioSettigs() 
	{
		audioSettigs.SetActive(true);
		characterSettigs.SetActive(false);
		eventSystem.SetSelectedGameObject(characterButton);
	}
}
