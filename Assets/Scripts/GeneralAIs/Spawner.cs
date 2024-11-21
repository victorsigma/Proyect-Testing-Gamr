using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private GameObject swarmerPrefab_1;
	[SerializeField]
	private GameObject swarmerPrefab_2;

	[SerializeField]
	private float swarmerInterval_1 = 3.5f;
	[SerializeField]
	private float swarmerInterval_2 = 3.5f;

	[SerializeField]
	private int maxEnemies_1 = 10;
	[SerializeField]
	private int maxEnemies_2 = 10;
	private int currentEnemyCount_1 = 0;
	private int currentEnemyCount_2 = 0;

	[SerializeField]
	private float spawnRangeX = 2f;
	[SerializeField]
	private float spawnRangeY = 2f;

	[SerializeField]
	private Sprite offSprite;
	[SerializeField]
	private Color offLight;

	[SerializeField]
	private Sprite enableSprite;
	[SerializeField]
	private Color enableLight;

	[SerializeField]
	private Sprite disableSprite;
	[SerializeField]
	private Color disableLight;
	[SerializeField]
	private GameObject lightSpawner;

	[SerializeField]
	private LayerMask detectorLayerMask; // Layer para detectar al jugador
	[SerializeField]
	private Vector2 detectorOriginOffset; // Offset desde la posición del spawner
	[SerializeField]
	private Vector2 detectorSize; // Tamaño del área de detección

	[SerializeField]
	private bool isSpawningActive = false;


	[SerializeField]
	public bool isSpawningEnd = false;

	void Start()
	{
		StartCoroutine(CheckSpawnCondition());
	}

	void Update()
	{
		if (isSpawningActive)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = enableSprite;
			lightSpawner.GetComponent<Light2D>().color = enableLight;
		}

		if (currentEnemyCount_1 != maxEnemies_1 && currentEnemyCount_2 != maxEnemies_2 && !isSpawningActive)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
			lightSpawner.GetComponent<Light2D>().color = offLight;
		}


		if (currentEnemyCount_1 == maxEnemies_1 && currentEnemyCount_2 == maxEnemies_2)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = disableSprite;
			lightSpawner.GetComponent<Light2D>().color = disableLight;
			isSpawningActive = false;
			StopAllCoroutines();
			if (!isSpawningEnd)
			{
				AudioManager.instance.PlaySFX("SpawnerOff");
			}
			isSpawningEnd = true;
		}
	}

	private IEnumerator CheckSpawnCondition()
	{
		while (true)
		{
			PerformDetection();
			yield return new WaitForSeconds(1f);
		}
	}

	private void PerformDetection()
	{
		// Define el tamaño del área de detección usando spawnRangeX y spawnRangeY
		Vector2 detectorSize = new Vector2(spawnRangeX * 2, spawnRangeY * 2);

		// Detecta si el jugador está dentro del radio usando OverlapBox
		Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);

		// Si detecta al jugador, activa el spawn
		if (collider != null && !isSpawningActive)
		{
			isSpawningActive = true;
			AudioManager.instance.PlaySFX("SpawnerOn");
			gameObject.GetComponent<OffscreenIndicator>().SetIndicatorActive(false);
			StartCoroutine(SpawnEnemyType1());
			StartCoroutine(SpawnEnemyType2());
		}
	}

	private IEnumerator SpawnEnemyType1()
	{
		while (isSpawningActive && currentEnemyCount_1 < maxEnemies_1)
		{
			yield return new WaitForSeconds(swarmerInterval_1);

			Instantiate(swarmerPrefab_1, new Vector3(transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
				transform.position.y + Random.Range(-spawnRangeY, spawnRangeY), 0), Quaternion.identity);
			currentEnemyCount_1++;
		}
	}

	private IEnumerator SpawnEnemyType2()
	{
		while (isSpawningActive && currentEnemyCount_2 < maxEnemies_2)
		{
			yield return new WaitForSeconds(swarmerInterval_2);

			Instantiate(swarmerPrefab_2, new Vector3(transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
				transform.position.y + Random.Range(-spawnRangeY, spawnRangeY), 0), Quaternion.identity);
			currentEnemyCount_2++;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, new Vector3(spawnRangeX * 2, spawnRangeY * 2, 0));
	}
}
