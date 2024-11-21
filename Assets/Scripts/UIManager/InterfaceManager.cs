using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScoreData
{
	public int score;
}

public class InterfaceManager : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	private GameObject mobileInterfase;
	[SerializeField]
	private GameObject pauseMenu;

	[SerializeField]
	private GameObject pauseMenuFirstButton;

	[SerializeField]
	private GameObject gameOverMenu;

	[SerializeField]
	private GameObject gameOverFirstButton;

	private CanvasGroup canvasMobile;

	private PlayerController playerController;

	private bool isGameOver = false;
	EventSystem eventSystem;
	
	private string saveFilePath;

	void Start()
	{
		saveFilePath = Application.persistentDataPath;
		canvasMobile = mobileInterfase.GetComponent<CanvasGroup>();

		playerController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();

		playerController.DeadPlayer += GameOver;

		eventSystem = EventSystem.current;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Pause") && !isGameOver)
		{
			if (Time.timeScale == 1f)
			{
				Pause();
			}
			else
			{
				Resume();
			}
		}
	}


	public void GameOver()
	{
		GameGlobals.uiStatus = "gameover";
		gameOverMenu.SetActive(true);
		canvasMobile.interactable = false;
		isGameOver = true;


		if (eventSystem != null && gameOverMenu != null && GameGlobals.lastInput == "joystick")
		{
			// Cambiar el target actual
			eventSystem.firstSelectedGameObject = gameOverFirstButton;
			eventSystem.SetSelectedGameObject(gameOverFirstButton);
		}
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		GameGlobals.uiStatus = "pause";
		if (GameGlobals.lastInput == "joystick")
		{
			eventSystem.firstSelectedGameObject = pauseMenuFirstButton;
			eventSystem.SetSelectedGameObject(pauseMenuFirstButton);
		}
		pauseMenu.SetActive(true);
		canvasMobile.interactable = false;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		GameGlobals.uiStatus = "none";
		pauseMenu.SetActive(false);
		canvasMobile.interactable = true;
	}

	public void Reload()
	{
		GameGlobals.uiStatus = "none";
		GameManager.instance.PlayerRebirth();
		GameManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Exit()
	{
		Time.timeScale = 1f;
		GameGlobals.uiStatus = "none";
		
		if(GameManager.instance.playerHealthCurrent == 0) 
		{
			GameManager.instance.PlayerRebirth();
		}
		
		ScoreData data = new();
		
		GameObject playerObject = GameObject.FindWithTag("Player");

		int score = playerObject.GetComponent<PlayerController>() == null ? 0 : playerObject.GetComponent<PlayerController>().GetScore();
		
		data.score = score;

		// Serializa a JSON
		string json = JsonUtility.ToJson(data);
		
		int indexCharacter = PlayerPrefs.GetInt("CharacterIndex");
		
		string filePath = Path.Combine(saveFilePath, GameManager.instance.characters[indexCharacter].name + ".json");
		
		File.WriteAllText(filePath, json);
		Debug.Log("Score guardado en: " + filePath);
		GameManager.instance.LoadScene("MainMenu");
	}
}
