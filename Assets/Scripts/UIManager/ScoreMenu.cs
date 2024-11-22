using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject mainFirstButton;

	[SerializeField]
	private GameObject[] elements;

	EventSystem eventSystem;
	void Start()
	{
		eventSystem = EventSystem.current;

		string customExtension = "*.score";

		// Obtén el directorio de persistentDataPath
		string directoryPath = Application.persistentDataPath;

		// Busca los archivos con la extensión específica
		string[] files = Directory.GetFiles(directoryPath, customExtension);

		List<(string fileName, float score)> scoreList = new List<(string fileName, float score)>();

		foreach (string file in files)
		{
			// Leer el contenido del archivo
			string jsonContent = File.ReadAllText(file);

			try
			{
				// Parsear el contenido JSON
				ScoreData data = JsonUtility.FromJson<ScoreData>(jsonContent);

				if (data != null)
				{
					// Obtener el nombre del archivo sin la extensión
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

					// Añadir los datos a la lista
					scoreList.Add((fileNameWithoutExtension, data.score));
				}
			}
			catch
			{
				Debug.LogError($"Error al parsear el archivo {file}");
			}
		}



		// Ordenar la lista por el score en orden descendente
		scoreList.Sort((a, b) => b.score.CompareTo(a.score));

		for (int i = 0; i < elements.Length; i++)
		{
			if(i == scoreList.Count) break;
			Transform name = elements[i].transform.Find("Name");
			if (name != null)
			{
				Transform txtName = name.Find("Text (TMP)");
				if (txtName != null)
				{
					TMP_Text textMeshPro = txtName.GetComponent<TMP_Text>();
					if (textMeshPro != null)
					{
						string text = scoreList[i].fileName ?? "Name";
						textMeshPro.text = text;
					}
				}
			}
			Transform score = elements[i].transform.Find("Score");
			if (score != null)
			{
				Transform txtScore = score.Find("Text (TMP)");
				if (txtScore != null)
				{
					TMP_Text textMeshPro = txtScore.GetComponent<TMP_Text>();
					if (textMeshPro != null)
					{
						string text = scoreList[i].score+"";
						textMeshPro.text = text;
					}
				}
			}
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			Cancel();
		}
	}


	public void Cancel()
	{
		eventSystem.SetSelectedGameObject(mainFirstButton);
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
	}
}
