using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{
	[SerializeField]
	private LifeBar lifeBar;
	
	private Enemy enemy;
	
	[SerializeField] private LootTable lootTable;
	void Start()
	{
		enemy = GetComponent<Enemy>();
		lifeBar.InitLifeBar(enemy.life, enemy.maxLife);
	}
	
	void Update() 
	{
		if (enemy.life <= 0)
		{
			DropLoot();
			Destroy(gameObject);
		}
	}

	private void DropLoot()
	{
		if (lootTable != null)
		{
			// Determina un número aleatorio de ítems a soltar
			int itemsToDrop = 1;

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
	
	public void ChangeCurrentLife(int life) 
	{
		lifeBar.ChangeCurrentLife(life);
	}
}
