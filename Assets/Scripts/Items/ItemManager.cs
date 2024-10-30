using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
	public TextAsset jsonFile; // Referencia al archivo JSON desde el inspector
	private ItemDatabase itemDatabase;

	void Start()
	{
		// Cargar el JSON desde el archivo
		itemDatabase = JsonUtility.FromJson<ItemDatabase>(jsonFile.text);
	}

	// Buscar un ítem por el nombre del Sprite
	public Item GetItemBySpriteName(string spriteName)
	{
		foreach (Item item in itemDatabase.items)
		{
			if (item.name == spriteName)
			{
				return item;
			}
		}
		return null; // Si no se encuentra
	}
}
