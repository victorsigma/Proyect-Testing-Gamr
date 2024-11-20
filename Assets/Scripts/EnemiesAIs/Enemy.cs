using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int life = 10;
	public int maxLife = 10;
	[SerializeField]
	private float elapsedTime = 0f;
	public Transform playerTransform;
	private bool isHit = false;
	private Animator animator;

	[SerializeField] private LootTable lootTable;
	[SerializeField] private int minDrops = 1; // Mínimo de ítems que puede soltar
	[SerializeField] private int maxDrops = 3; // Máximo de ítems que puede soltar

	[SerializeField] private string sound = "DamageMeet";

	[SerializeField] private List<SpawnEvent> spawnEvents;

	[SerializeField] private bool isBoss = false;

	[SerializeField] private GameObject teleportMap;

	void Start()
	{
		animator = GetComponent<Animator>();
		FindPlayer();
	}


	void Update()
	{
		if (playerTransform == null)
		{
			FindPlayer();
		}
		ActiveSpawnEvents();
	}

	void ActiveSpawnEvents()
	{
		foreach (SpawnEvent spawnEvent in spawnEvents)
		{
			if (!spawnEvent.CanActivate()) continue;

			float healthPercentage = (float)life / maxLife * 100f;
			elapsedTime += Time.deltaTime;

			switch (spawnEvent.activationType)
			{
				case SpawnActivationType.HealthPercentage:
					if (spawnEvent.ShouldActivateByHealth(healthPercentage))
					{
						TriggerSpawnEvent(spawnEvent);
					}
					break;

				case SpawnActivationType.TimeElapsed:
					if (spawnEvent.ShouldActivateByTime(elapsedTime))
					{
						TriggerSpawnEvent(spawnEvent);
					}
					break;

				case SpawnActivationType.OnDamageTaken:
					if (isHit)
					{
						TriggerSpawnEvent(spawnEvent);
					}
					break;

				case SpawnActivationType.PlayerProximity:
					if (spawnEvent.ShouldActivateByProximity(transform, playerTransform))
					{
						TriggerSpawnEvent(spawnEvent);
					}
					break;
			}
		}
	}

	void TriggerSpawnEvent(SpawnEvent spawnEvent)
	{
		foreach (var enemyData in spawnEvent.enemiesToSpawn)
		{
			for (int i = 0; i < enemyData.amount; i++)
			{
				Instantiate(enemyData.enemyPrefab, transform.position, Quaternion.identity);
			}
		}
		spawnEvents.Find(x => x == spawnEvent).MarkAsUsed();
		elapsedTime = 0;
	}

	public void TakeDamage(int damage)
	{
		life -= damage;

		AudioManager.instance.PlaySFX(sound);
		isHit = true;
		animator.SetBool("isHit", isHit);
		
		if(isBoss) 
		{
			BossBar bossBar = GetComponent<BossBar>();
			bossBar.ChangeCurrentLife(life);
		}

		if (life <= 0)
		{
			DropLoot(); // Llama a la función para soltar botín
			if (isBoss)
			{
				Instantiate(teleportMap, gameObject.transform.position, Quaternion.identity);
			} else 
			{
				Destroy(gameObject);
			}
		}
	}

	private void DropLoot()
	{
		if (lootTable != null)
		{
			// Determina un número aleatorio de ítems a soltar
			int itemsToDrop = Random.Range(minDrops, maxDrops + 1);

			for (int i = 0; i < itemsToDrop; i++)
			{
				GameObject loot = lootTable.GetRandomItem();
				if (loot != null)
				{
					// Genera el ítem en una posición cercana al enemigo para dispersarlos ligeramente
					Vector3 dropPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
					Instantiate(loot, dropPosition, Quaternion.identity);
				}
			}
		}
	}

	public void SetNotHit()
	{
		isHit = false;
		animator.SetBool("isHit", isHit);
	}

	void FindPlayer()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			playerTransform = playerObject.transform;
		}
	}
}
