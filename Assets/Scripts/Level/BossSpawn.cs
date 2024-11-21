using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
	public string targetTag = "Spawner";

	[SerializeField]
	private string levelSound = "Exploration";

	[SerializeField]
	private string bossSound = "Boss";

	[SerializeField]
	private GameObject boss;

	[SerializeField]
	private bool isSpawningEnd = false;

	private Coroutine checkSpawningCoroutine;

	[SerializeField]
	private GameObject progressBar;

	private float spawnerAmount = 0;
	private float spawnersCompleted = 0;

	void Start()
	{
		AudioManager.instance.PlayMusic(levelSound);
		gameObject.GetComponent<OffscreenIndicator>().SetIndicatorActive(false);

		// Inicializa la barra de progreso
		Slider slider = progressBar.GetComponent<Slider>();
		if (slider != null)
		{
			slider.value = 0;
		}

		// Iniciar la corrutina para comprobar el estado de los Spawner
		checkSpawningCoroutine = StartCoroutine(CheckSpawningEndCoroutine());
	}

	IEnumerator CheckSpawningEndCoroutine()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
		spawnerAmount = objects.Length; // Define el total de spawners desde el inicio

		while (!isSpawningEnd)
		{
			spawnersCompleted = 0; // Reinicia el conteo

			// Cuenta los spawners completados
			foreach (GameObject obj in objects)
			{
				if (obj.GetComponent<Spawner>().isSpawningEnd)
				{
					spawnersCompleted++;
				}
			}
			print(spawnersCompleted / spawnerAmount * 100);

			// Actualiza la barra de progreso
			UpdateProgressBar();

			// Verifica si todos los spawners han completado su trabajo
			if (spawnersCompleted == spawnerAmount)
			{
				ExecuteEvent();
				yield break; // Salir de la corrutina una vez que se ejecuta el evento
			}

			// Esperar 0.5 segundos antes de volver a comprobar
			yield return new WaitForSeconds(0.5f);
		}
	}

	void UpdateProgressBar()
	{
		Slider slider = progressBar.GetComponent<Slider>();
		if (slider != null)
		{
			slider.value = spawnersCompleted / spawnerAmount * 100; // Actualiza el valor entre 0 y 1
		}
	}

	void ExecuteEvent()
	{
		isSpawningEnd = true;
		gameObject.GetComponent<OffscreenIndicator>().SetIndicatorActive(true);
		Instantiate(boss, transform.position, Quaternion.identity);
		AudioManager.instance.PlayMusic(bossSound);

		// Detener la corrutina al finalizar
		if (checkSpawningCoroutine != null)
		{
			StopCoroutine(checkSpawningCoroutine);
			checkSpawningCoroutine = null;
		}
	}

	private void OnDestroy()
	{
		// Asegurarse de detener la corrutina si el objeto se destruye
		if (checkSpawningCoroutine != null)
		{
			StopCoroutine(checkSpawningCoroutine);
		}
	}
}
