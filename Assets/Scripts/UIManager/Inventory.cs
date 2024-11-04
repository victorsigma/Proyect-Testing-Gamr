using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryData
{
	public List<string> itemSpritesInventory; // Almacena los nombres de las sprites en los slots
	public List<string> itemSpritesEquipment; // Almacena los nombres de las sprites en los slots
	public int equippedSlot; // Almacena el índice del slot equipado
}

public class Inventory : MonoBehaviour
{
	public List<GameObject> slots = new List<GameObject>();
	public GameObject inv;
	public bool isActive;
	public GameObject selector;
	public int slot;
	public List<GameObject> equipments = new List<GameObject>();
	public int equipmentSlot;

	public bool isEquipment;

	public bool editEquipment;
	public bool editInventory;

	[SerializeField]
	private Sprite emptySlot;
	public List<Color> selectionColors;
	public GameObject optionsInventory;
	public List<GameObject> selectionsInventory;
	public int selectionInventory;
	public bool isOptionsInventory;

	public GameObject optionsEquipament;
	public List<GameObject> selectionsEquipament;
	public int selectionEquipament;
	public bool isOptionsEquipament;

	private bool inputProcessed = false;

	public GameObject equipmentBar;

	public GameObject equipmentBarSelector;

	public int selectionEquipmentBar;

	public List<GameObject> equipmentBarSlots = new List<GameObject>();
	public bool leftClick;
	public bool rightClick;
	private float previousRightTriggerValue = 0.0f;

	public GameObject hand;
	private string saveFilePath;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Item"))
		{
			Sprite sprite = collider.GetComponent<SpriteRenderer>().sprite;
			Destroy(collider.gameObject);
			for (int i = 0; i < slots.Count; i++)
			{
				if (IsEmpty(slots[i].GetComponent<Image>()))
				{
					slots[i].GetComponent<Image>().sprite = sprite;
					//slots[i].GetComponent<Image>().sprite.name = collider.GetComponent<SpriteRenderer>().sprite.name;
					break;
				}
			}
		}
	}

	public void SaveInventory()
	{
		InventoryData data = new InventoryData();
		data.itemSpritesInventory = new List<string>();
		data.itemSpritesEquipment = new List<string>();

		// Guarda los sprites de los slots
		foreach (var slot in slots)
		{
			var sprite = slot.GetComponent<Image>().sprite;
			if (sprite.name.Split(" ").Length > 1)
			{
				if (sprite != null && sprite.name != "Empty")
				{
					string firstWord = sprite.name.Split(' ')[0];
					data.itemSpritesInventory.Add("Items/" + firstWord + "s/" + sprite.name);
				}
				else
				{
					data.itemSpritesInventory.Add("Items/Empty");
				}
			} else 
			{
				if (sprite != null && sprite.name != "Empty")
				{
					data.itemSpritesInventory.Add("Items/" + sprite.name);
				}
				else
				{
					data.itemSpritesInventory.Add("Items/Empty");
				}
			}
		}

		foreach (var slot in equipments)
		{
			var sprite = slot.GetComponent<Image>().sprite;
			if (sprite != null && sprite.name != "Empty")
			{
				string firstWord = sprite.name.Split(' ')[0];
				data.itemSpritesEquipment.Add("Items/" + firstWord + "s/" + sprite.name);
			}
			else
			{
				data.itemSpritesEquipment.Add("Items/Empty");
			}
		}

		// Guarda el slot equipado
		data.equippedSlot = equipmentSlot;

		// Serializa a JSON
		string json = JsonUtility.ToJson(data);
		File.WriteAllText(saveFilePath, json);
		Debug.Log("Inventario guardado en: " + saveFilePath);
	}

	public void LoadInventory()
	{
		if (File.Exists(saveFilePath))
		{
			string json = File.ReadAllText(saveFilePath);
			InventoryData data = JsonUtility.FromJson<InventoryData>(json);

			// Carga los sprites de los slots
			for (int i = 0; i < slots.Count; i++)
			{
				if (data.itemSpritesInventory[i] != "Empty")
				{
					// Cargar el sprite por su nombre
					Sprite loadedSprite = Resources.Load<Sprite>(data.itemSpritesInventory[i]);
					slots[i].GetComponent<Image>().sprite = loadedSprite;
				}
				else
				{
					slots[i].GetComponent<Image>().sprite = emptySlot;
				}
			}

			// Carga los sprites de los slots
			for (int i = 0; i < equipments.Count; i++)
			{
				if (data.itemSpritesEquipment[i] != "Empty")
				{
					// Cargar el sprite por su nombre
					Sprite loadedSprite = Resources.Load<Sprite>(data.itemSpritesEquipment[i]);
					equipments[i].GetComponent<Image>().sprite = loadedSprite;
				}
				else
				{
					equipments[i].GetComponent<Image>().sprite = emptySlot;
				}
			}

			// Carga el slot equipado
			equipmentSlot = data.equippedSlot;
			Debug.Log("Inventario cargado desde: " + saveFilePath);
		}
		else
		{
			Debug.LogWarning("No se encontró el archivo de inventario.");
		}
	}

	private void OnApplicationQuit()
	{
		SaveInventory(); // Guarda el inventario al salir
	}

	bool IsEmpty(Image image)
	{
		return image.sprite.name == "Empty";
	}

	public void NavegationInventory()
	{
		if (GameGlobals.inventoryOn)
		{
			// Solo procesar una vez por cada entrada horizontal
			if (Input.GetAxisRaw("InventoryHorizontal") > 0 && !inputProcessed && slot < 15)
			{
				slot++;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}
			else if (Input.GetAxisRaw("InventoryHorizontal") < 0 && !inputProcessed && slot > 0)
			{
				slot--;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}

			// Solo procesar una vez por cada entrada vertical
			if (Input.GetAxisRaw("InventoryVertical") > 0 && slot > 3 && !inputProcessed)
			{
				slot -= 4;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}
			else if (Input.GetAxisRaw("InventoryVertical") < 0 && slot < 12 && !inputProcessed)
			{
				slot += 4;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}

			// Reiniciamos el estado de entrada si no hay entrada
			if (Input.GetAxisRaw("InventoryHorizontal") == 0 && Input.GetAxisRaw("InventoryVertical") == 0)
			{
				inputProcessed = false;
			}

			selector.transform.position = slots[slot].transform.position;

			if (Input.GetButtonDown("Confirm"))
			{
				Unequip();
			}
		}
	}

	void Unequip()
	{
		if (editInventory)
		{
			if (IsEmpty(slots[slot].GetComponent<Image>()))
			{
				slots[slot].GetComponent<Image>().sprite = equipments[equipmentSlot].GetComponent<Image>().sprite;
				equipments[equipmentSlot].GetComponent<Image>().sprite = emptySlot;
			}
			else
			{
				Sprite lastSprite = slots[slot].GetComponent<Image>().sprite;
				slots[slot].GetComponent<Image>().sprite = equipments[equipmentSlot].GetComponent<Image>().sprite;
				equipments[equipmentSlot].GetComponent<Image>().sprite = lastSprite;
			}
			editInventory = false;
		}
	}

	public void NavegationEquipments()
	{
		if (GameGlobals.inventoryOn)
		{
			// Solo procesar una vez por cada entrada vertical

			if (Input.GetAxisRaw("InventoryVertical") > 0 && equipmentSlot > 0 && !inputProcessed)
			{
				equipmentSlot--;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}
			else if (Input.GetAxisRaw("InventoryVertical") < 0 && equipmentSlot < equipments.Count - 1 && !inputProcessed)
			{
				equipmentSlot++;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}

			// Reiniciamos el estado de entrada si no hay entrada
			if (Input.GetAxisRaw("InventoryVertical") == 0)
			{
				inputProcessed = false;
			}

			selector.transform.position = equipments[equipmentSlot].transform.position;

			if (Input.GetButtonDown("Confirm"))
			{
				Equip();
			}
		}
	}

	void Equip()
	{
		if (!editEquipment)
		{
			if (IsEmpty(equipments[equipmentSlot].GetComponent<Image>()))
			{
				equipments[equipmentSlot].GetComponent<Image>().sprite = slots[slot].GetComponent<Image>().sprite;
				slots[slot].GetComponent<Image>().sprite = emptySlot;
			}
			else
			{
				Sprite lastSprite = equipments[equipmentSlot].GetComponent<Image>().sprite;
				equipments[equipmentSlot].GetComponent<Image>().sprite = slots[slot].GetComponent<Image>().sprite;
				slots[slot].GetComponent<Image>().sprite = lastSprite;
			}

			isEquipment = false;
		}
	}

	public void NavegationOptionsInventory()
	{
		if (GameGlobals.inventoryOn)
		{
			// Solo procesar una vez por cada entrada vertical
			if (Input.GetAxisRaw("InventoryVertical") > 0 && selectionInventory > 0 && !inputProcessed)
			{
				selectionInventory--;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}
			else if (Input.GetAxisRaw("InventoryVertical") < 0 && selectionInventory < selectionsInventory.Count - 1 && !inputProcessed)
			{
				selectionInventory++;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}

			// Reiniciamos el estado de entrada si no hay entrada
			if (Input.GetAxisRaw("InventoryVertical") == 0)
			{
				inputProcessed = false;
			}

			switch (selectionInventory)
			{
				case 0:
					selectionsInventory[0].GetComponent<Image>().color = selectionColors[1];
					selectionsInventory[1].GetComponent<Image>().color = selectionColors[0];
					break;
				case 1:
					selectionsInventory[0].GetComponent<Image>().color = selectionColors[0];
					selectionsInventory[1].GetComponent<Image>().color = selectionColors[1];
					break;
			}

			if (Input.GetButtonDown("Confirm"))
			{
				OptionsInventorySelection();
			}
		}
	}

	public void OptionsInventory(int selection)
	{
		selectionInventory = selection;
		switch (selectionInventory)
		{
			case 0:
				selectionsInventory[0].GetComponent<Image>().color = selectionColors[1];
				selectionsInventory[1].GetComponent<Image>().color = selectionColors[0];
				break;
			case 1:
				selectionsInventory[0].GetComponent<Image>().color = selectionColors[0];
				selectionsInventory[1].GetComponent<Image>().color = selectionColors[1];
				break;
		}
		OptionsInventorySelection();
	}

	void OptionsInventorySelection()
	{
		switch (selectionInventory)
		{
			case 0:
				isEquipment = true;
				isOptionsInventory = false;
				break;
			case 1:
				GameObject prefab;
				var sprite = slots[slot].GetComponent<Image>().sprite;
				string firstWord = sprite.name.Split(' ')[0];

				if (sprite.name.Split(" ").Length > 1)
				{
					string prefabPath = "Prefabs/Items/" + firstWord + "s/" + sprite.name;

					prefab = Resources.Load<GameObject>(prefabPath);

					if (prefab != null)
					{
						// Instancia el prefab en la escena
						Vector3 spawn = gameObject.transform.position;

						spawn.x += 0.5f;

						slots[slot].GetComponent<Image>().sprite = emptySlot;
						Instantiate(prefab, spawn, Quaternion.identity);
						isOptionsInventory = false;
					}
					else
					{
						Debug.LogError("No se pudo cargar el prefab.");
						isOptionsInventory = false;
					}
				}
				else
				{
					string prefabPath = "Prefabs/Items/" + sprite.name;

					prefab = Resources.Load<GameObject>(prefabPath);

					if (prefab != null)
					{
						// Instancia el prefab en la escena
						Vector3 spawn = gameObject.transform.position;

						spawn.x += 0.5f;

						slots[slot].GetComponent<Image>().sprite = emptySlot;
						Instantiate(prefab, spawn, Quaternion.identity);
						isOptionsInventory = false;
					}
					else
					{
						Debug.LogError("No se pudo cargar el prefab.");
						isOptionsInventory = false;
					}
				}
				break;
		}
	}

	public void NavegationOptionsEquipament()
	{
		if (GameGlobals.inventoryOn)
		{
			// Solo procesar una vez por cada entrada vertical
			if (Input.GetAxisRaw("InventoryVertical") > 0 && selectionEquipament > 0 && !inputProcessed)
			{
				selectionEquipament--;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}
			else if (Input.GetAxisRaw("InventoryVertical") < 0 && selectionEquipament < selectionsEquipament.Count - 1 && !inputProcessed)
			{
				selectionEquipament++;
				inputProcessed = true; // Marcamos que hemos procesado la entrada
			}

			// Reiniciamos el estado de entrada si no hay entrada
			if (Input.GetAxisRaw("InventoryVertical") == 0)
			{
				inputProcessed = false;
			}

			switch (selectionEquipament)
			{
				case 0:
					selectionsEquipament[0].GetComponent<Image>().color = selectionColors[1];
					selectionsEquipament[1].GetComponent<Image>().color = selectionColors[0];
					break;
				case 1:
					selectionsEquipament[0].GetComponent<Image>().color = selectionColors[0];
					selectionsEquipament[1].GetComponent<Image>().color = selectionColors[1];
					break;
			}

			if (Input.GetButtonDown("Confirm"))
			{
				OptionsEquipamentSelection();
			}
		}
	}

	public void OptionsEquipament(int selection)
	{
		selectionEquipament = selection;
		switch (selectionEquipament)
		{
			case 0:
				selectionsEquipament[0].GetComponent<Image>().color = selectionColors[1];
				selectionsEquipament[1].GetComponent<Image>().color = selectionColors[0];
				break;
			case 1:
				selectionsEquipament[0].GetComponent<Image>().color = selectionColors[0];
				selectionsEquipament[1].GetComponent<Image>().color = selectionColors[1];
				break;
		}
		OptionsEquipamentSelection();
	}

	void OptionsEquipamentSelection()
	{
		switch (selectionEquipament)
		{
			case 0:
				isEquipment = false;
				editEquipment = false;
				editInventory = true;
				isOptionsEquipament = false;
				break;
			case 1:
				GameObject prefab;
				var sprite = equipments[equipmentSlot].GetComponent<Image>().sprite;
				string firstWord = sprite.name.Split(' ')[0];
				if (sprite.name.Split(" ").Length > 1)
				{
					string prefabPath = "Prefabs/Items/" + firstWord + "s/" + sprite.name;

					prefab = Resources.Load<GameObject>(prefabPath);

					if (prefab != null)
					{
						// Instancia el prefab en la escena
						Vector3 spawn = gameObject.transform.position;

						spawn.x += 0.5f;

						equipments[equipmentSlot].GetComponent<Image>().sprite = emptySlot;
						Instantiate(prefab, spawn, Quaternion.identity);
						isOptionsEquipament = false;
					}
					else
					{
						Debug.LogError("No se pudo cargar el prefab.");
						isOptionsEquipament = false;
					}
				}
				else
				{
					string prefabPath = "Prefabs/Items/" + sprite.name;

					prefab = Resources.Load<GameObject>(prefabPath);

					if (prefab != null)
					{
						// Instancia el prefab en la escena
						Vector3 spawn = gameObject.transform.position;

						spawn.x += 0.5f;

						equipments[equipmentSlot].GetComponent<Image>().sprite = emptySlot;
						Instantiate(prefab, spawn, Quaternion.identity);
						isOptionsEquipament = false;
					}
					else
					{
						Debug.LogError("No se pudo cargar el prefab.");
						isOptionsEquipament = false;
					}
				}
				break;

		}
	}


	public void SetSelectionInventory(int selection)
	{
		if (GameGlobals.lastInput == "touch")
		{
			if (!isEquipment && !isOptionsInventory)
			{
				slot = selection;

				Unequip();
			}
		}
	}

	public void SetSelectionEquipament(int selection)
	{
		if (GameGlobals.lastInput == "touch")
		{
			if (isEquipment && !isOptionsEquipament)
			{
				equipmentSlot = selection;

				Equip();
			}
		}
	}

	public void SwichMode()
	{
		if (!isOptionsEquipament && !isOptionsInventory && !editInventory)
		{
			if (isEquipment)
			{
				if (!editEquipment)
				{
					isEquipment = true;
					editEquipment = false;
				}
				else
				{
					isEquipment = false;
					editEquipment = false;
				}
			}
			else
			{
				if (!editEquipment)
				{
					isEquipment = true;
					editEquipment = true;
				}
				else
				{
					isEquipment = false;
					editEquipment = false;
				}
			}
		}
	}


	public void OpenClose()
	{
		if (GameGlobals.uiStatus == "none")
		{
			isActive = !isActive;
		}
	}


	public void OpenOptionsMobile()
	{
		if (GameGlobals.lastInput == "touch")
		{
			if (!editEquipment && !isEquipment && !editInventory && !IsEmpty(slots[slot].GetComponent<Image>()))
			{
				isOptionsInventory = !isOptionsInventory;
			}

			if (!editEquipment && !isEquipment && editInventory)
			{
				SetSelectionInventory(slot);
			}

			if (editEquipment && isEquipment && !IsEmpty(equipments[equipmentSlot].GetComponent<Image>()))
			{
				isOptionsEquipament = !isOptionsEquipament;
			}

			if (!editEquipment && isEquipment)
			{
				SetSelectionEquipament(equipmentSlot);
			}
		}
	}
	void OpenOptions()
	{
		if (!editEquipment && !isEquipment && !IsEmpty(slots[slot].GetComponent<Image>()))
		{
			isOptionsInventory = !isOptionsInventory;
		}

		if (editEquipment && isEquipment && !IsEmpty(equipments[equipmentSlot].GetComponent<Image>()))
		{
			isOptionsEquipament = !isOptionsEquipament;
		}
	}

	void EquipamentBarManager()
	{


		float scrollInput = Input.GetAxisRaw("Mouse ScrollWheel");

		for (int i = 0; i < equipments.Count; i++)
		{
			equipmentBarSlots[i].GetComponent<Image>().sprite = equipments[i].GetComponent<Image>().sprite;
		}

		if (!isActive)
		{
			if (Input.GetButtonDown("EquipamentBarRight") || scrollInput < 0f)
			{
				EquipamentBarSelectionUp();
			}

			if (Input.GetButtonDown("EquipamentBarLeft") || scrollInput > 0f)
			{
				EquipamentBarSelectionDown();
			}
		}

		for (int i = 0; i <= 9; i++)
		{
			if (Input.GetKeyDown(i.ToString()))
			{

				if (i < equipmentBarSlots.Count + 1)
				{
					selectionEquipmentBar = i - 1;
				}
			}
		}

		equipmentBarSelector.transform.position = equipmentBarSlots[selectionEquipmentBar].transform.position;

		leftClick = false;
		rightClick = false;

		float leftTriggerValue = Input.GetAxis("LeftTrigger");
		// Leer el valor de Right Trigger (RT)
		float rightTriggerValue = Input.GetAxis("RightTrigger");

		bool leftTriggerPulse = leftTriggerValue > 0;
		bool rightTriggerPulse = previousRightTriggerValue <= 0.6 && rightTriggerValue >= 0.7;

		// Actualizar el estado anterior del Right Trigger para usarlo en el próximo frame
		previousRightTriggerValue = rightTriggerValue;

		hand.GetComponent<SpriteRenderer>().sprite = equipments[selectionEquipmentBar].GetComponent<Image>().sprite;
		if (GameGlobals.lastInput != "touch")
		{
			if (Input.GetButtonDown("FireButton") || rightTriggerPulse)
			{
				UseItem();
			}
		}
	}

	public void UseItem()
	{
		hand.SetActive(true);
		hand.GetComponent<Animator>().SetTrigger("Attack");
		if (GameManager.instance.UseItem(equipments[selectionEquipmentBar].GetComponent<Image>().sprite.name))
		{
			equipments[selectionEquipmentBar].GetComponent<Image>().sprite = emptySlot;
		}
		//hand.SetActive(false);
	}

	public void SetSelectionEquipamentBar(int selection)
	{
		if (GameGlobals.lastInput == "touch")
		{
			selectionEquipmentBar = selection;
		}
	}

	void EquipamentBarSelectionUp()
	{
		if (selectionEquipmentBar == equipmentBarSlots.Count - 1)
		{
			selectionEquipmentBar = 0;
		}
		else
		{
			selectionEquipmentBar++;
		}
	}

	void EquipamentBarSelectionDown()
	{
		if (selectionEquipmentBar == 0)
		{
			selectionEquipmentBar = equipmentBarSlots.Count - 1;
		}
		else
		{
			selectionEquipmentBar--;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
		LoadInventory(); // Carga el inventario al inicio
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			OpenClose();
		}



		if (isActive)
		{
			inv.SetActive(true);
			GameGlobals.inventoryOn = true;
			if (Input.GetButtonDown("InventoryEquipament"))
			{
				OpenOptions();
			}

			if (Input.GetButtonDown("InventorySwich"))
			{
				SwichMode();
			}
		}
		else
		{
			inv.SetActive(false);
			GameGlobals.inventoryOn = false;
			isOptionsInventory = false;
			isOptionsEquipament = false;
			isEquipment = false;
			editEquipment = false;
			editInventory = false;
		}

		EquipamentBarManager();

		optionsInventory.SetActive(isOptionsInventory);
		if (isOptionsInventory)
		{
			optionsInventory.transform.position = slots[slot].transform.position;
		}

		optionsEquipament.SetActive(isOptionsEquipament);
		if (isOptionsEquipament)
		{
			optionsEquipament.transform.position = equipments[equipmentSlot].transform.position;
		}

		if (GameGlobals.uiStatus == "none")
		{
			if (!isEquipment && !isOptionsInventory)
			{
				NavegationInventory();
			}

			if (isEquipment && !isOptionsEquipament)
			{
				NavegationEquipments();
			}

			if (isOptionsInventory)
			{
				NavegationOptionsInventory();
			}

			if (isOptionsEquipament)
			{
				NavegationOptionsEquipament();
			}
		}
	}
}
