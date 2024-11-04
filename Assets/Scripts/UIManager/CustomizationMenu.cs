using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
	private int index;
	[SerializeField]
	private Image characterImage;
	[SerializeField]

	private Animator animatorController;
	[SerializeField]
	private TextMeshProUGUI characterName;
	[SerializeField]
	private GameObject mainMenu;
	
	[SerializeField]
	private GameObject mainFirstButton;

	EventSystem eventSystem;


	// Start is called before the first frame update
	void Start()
	{
		eventSystem = EventSystem.current;
		index = PlayerPrefs.GetInt("CharacterIndex");

		ChangeDisplay();
	}
	
	public void Reload()
	{
		index = PlayerPrefs.GetInt("CharacterIndex");
		ChangeDisplay();
	}

	private void ChangeDisplay()
	{
		characterImage.sprite = GameManager.instance.characters[index].characterImage;
		characterName.text = GameManager.instance.characters[index].characterName;
		animatorController.runtimeAnimatorController = GameManager.instance.characters[index].animatorController;
	}

	public void NextCharacter()
	{
		if (index == GameManager.instance.characters.Count - 1)
		{
			index = 0;
		}
		else
		{
			index += 1;
		}

		ChangeDisplay();
	}

	public void PreviousCharacter()
	{
		if (index == 0)
		{
			index = GameManager.instance.characters.Count - 1;
		}
		else
		{
			index -= 1;
		}

		ChangeDisplay();
	}

	// Update is called once per frame
	void Update()
	{
		float scrollInput = Input.GetAxisRaw("Mouse ScrollWheel");
		if (Input.GetButtonDown("EquipamentBarRight") || scrollInput < 0f)
		{
			NextCharacter();
		}

		if (Input.GetButtonDown("EquipamentBarLeft") || scrollInput > 0f)
		{
			PreviousCharacter();
		}
		
		if(Input.GetButtonDown("Cancel")) 
		{
			Cancel();
		}
	}
	
	public void Submit() 
	{
		PlayerPrefs.SetInt("CharacterIndex", index);
		eventSystem.SetSelectedGameObject(mainFirstButton);
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
	}
	
	public void Cancel()
	{
		eventSystem.SetSelectedGameObject(mainFirstButton);
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
	}
}
