using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 10;
    private Animator animator;

    [SerializeField] private LootTable lootTable;
    [SerializeField] private int minDrops = 1; // Mínimo de ítems que puede soltar
    [SerializeField] private int maxDrops = 3; // Máximo de ítems que puede soltar

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        animator.SetBool("isHit", true);

        if (life <= 0)
        {
            DropLoot(); // Llama a la función para soltar botín
            Destroy(gameObject);
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
        animator.SetBool("isHit", false);
    }
}
