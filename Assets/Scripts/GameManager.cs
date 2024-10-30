using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Singleton del GameManager
	public static GameManager instance;

	// Variables globales del juego
	public int playerHealthMax = 20;
	public int playerHealthCurrent = 20;

	public int playerSpeedCurrent = 3;

	public int playerSpeedOriginal = 3;

	public ItemManager itemManager;
	//public Inventory inventory;

	public delegate void EventHandler();
	public event EventHandler HealthChange;

	public float timeRemaining = 10f;  // El tiempo que quieres contar (en segundos)
	public bool timerIsRunning = false;

	public GameObject loadScreen;

	public GameObject loadTips;

	void Awake()
	{
		// Asegura que solo haya una instancia del GameManager
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject); // Mantener el GameManager entre escenas
		}
		else
		{
			Destroy(gameObject); // Elimina instancias adicionales
		}
	}

	void Start()
	{
		// Inicialización del itemManager y el inventario
		if (itemManager == null)
		{
			itemManager = FindObjectOfType<ItemManager>();
		}

		/*if (inventory == null)
		{
			inventory = FindObjectOfType<Inventory>();
		}*/
	}

	// Método para modificar la salud del jugador
	public void ModifyPlayerHealth(int amount)
	{
		playerHealthCurrent += amount;
		playerHealthCurrent = Mathf.Clamp(playerHealthCurrent, 0, playerHealthMax); // Asegura que la salud esté entre 0 y 100
		HealthChange?.Invoke();
	}

	public void ModifyPlayerSpeed(int amount, int duration)
	{
		playerSpeedCurrent += amount;
		timeRemaining = duration;
		timerIsRunning = true;
		playerSpeedCurrent = Mathf.Clamp(playerSpeedCurrent, 0, 10); // Asegura que la salud esté entre 0 y 100
	}

	public void PlayerRebirth()
	{
		playerHealthCurrent = playerHealthMax;
	}

	// Método para usar un item y aplicar efectos al jugador
	public bool UseItem(string spriteName)
	{
		Item item = itemManager.GetItemBySpriteName(spriteName);

		if (item != null)
		{
			if (item.consumable.healing != 0 || item.consumable.speed != 0)
			{
				ModifyPlayerHealth(item.consumable.healing);
				ModifyPlayerSpeed(item.consumable.speed, item.consumable.duration);
				Debug.Log($"Usaste {item.name}. Salud: {playerHealthCurrent}, Velocidad: {playerSpeedCurrent}");
				return true;
			}
			if (item.wepon.damage != 0)
			{
				Debug.Log($"Equipaste el arma: {item.name} con {item.wepon.damage} de daño");
				// Aquí podrías actualizar el estado de combate del jugador
				return false;
			}
			if (item.equipment.resistance != 0)
			{
				Debug.Log($"Equipaste equipo con {item.equipment.resistance} de resistencia");
				// Aquí podrías modificar la resistencia o defensa del jugador
				return false;
			}
		}
		else
		{
			Debug.Log("No se encontró el ítem.");
			return false;
		}
		return false;
	}

	public void LoadScene(String name)
	{
		loadTips.GetComponent<TMP_Text>().text = gameObject.GetComponent<TipsManager>().GetRandomTip();
		StartCoroutine(SetScene(name));
	}

	public void LoadScene(int index)
	{
		loadTips.GetComponent<TMP_Text>().text = gameObject.GetComponent<TipsManager>().GetRandomTip();
		StartCoroutine(SetScene(index));
	}

	public IEnumerator SetScene(String name)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(name);

		while (!operation.isDone)
		{
			loadScreen.SetActive(true);
			yield return null;
		}
		loadScreen.SetActive(false);
	}

	public IEnumerator SetScene(int index)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(index);

		while (!operation.isDone)
		{
			loadScreen.SetActive(true);
			yield return null;
		}
		loadScreen.SetActive(false);
	}

	void Update()
	{
		if (timerIsRunning)
		{
			if (timeRemaining > 0)
			{
				// Reduce el tiempo restante
				timeRemaining -= Time.deltaTime;
			}
			else
			{
				// Detén el temporizador si el tiempo llega a 0
				Debug.Log("El tiempo se ha acabado");
				playerSpeedCurrent = playerSpeedOriginal;
				timeRemaining = 0;
				timerIsRunning = false;
			}
		}
	}

	// Método para guardar el estado del juego
	public void SaveGame()
	{
		// Aquí podrías guardar el estado del jugador, inventario, etc.
		Debug.Log("Juego guardado");
	}

	// Método para cargar el estado del juego
	public void LoadGame()
	{
		// Aquí podrías cargar el estado del jugador, inventario, etc.
		Debug.Log("Juego cargado");
	}
}
