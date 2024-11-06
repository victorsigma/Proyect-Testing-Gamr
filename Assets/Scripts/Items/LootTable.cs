using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab; // Prefab del ítem
    [Range(0, 100)] public float dropChance; // Probabilidad de generación en porcentaje (0 a 100)
}

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<LootItem> lootItems; // Lista de ítems en la tabla de botín

    // Método para obtener un ítem aleatorio en función de sus probabilidades
    public GameObject GetRandomItem()
    {
        foreach (LootItem lootItem in lootItems)
        {
            // Genera un número aleatorio entre 0 y 100
            float randomValue = Random.Range(0f, 100f);

            // Si el valor aleatorio es menor o igual a la probabilidad de drop, devuelve el prefab
            if (randomValue <= lootItem.dropChance)
            {
                return lootItem.itemPrefab;
            }
        }

        // Si ningún ítem es seleccionado, devuelve null
        return null;
    }
}
