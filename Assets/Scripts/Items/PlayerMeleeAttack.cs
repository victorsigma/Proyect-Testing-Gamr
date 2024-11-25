using System.Collections;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
	public float attackRange = 1.0f; // Rango del ataque
	public float attackCooldown = 1f; // Tiempo de enfriamiento entre ataques
	public bool isAttacking = false;

	public Transform attackPoint; // El punto desde donde se mide el alcance del ataque
	public LayerMask enemyLayers; // Capa que indica los objetos que pueden recibir daño (enemigos)
	private Animator animator; // Componente Animator para manejar las animaciones

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Attack(int attackDamage)
	{
		// Reproduce la animación de ataque
		if (!isAttacking)
		{
			isAttacking = true;
			//animator.SetTrigger("Attack");
			Vector3 position = attackPoint.position;

			position.y += 0.25f;

			// Detecta todos los enemigos en el área del ataque
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(position, attackRange, enemyLayers);

			// Aplica daño a cada enemigo detectado
			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
				enemy.GetComponent<EnemyTutorial>()?.TakeDamage(attackDamage);
			}
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "EnemyBullet")
		{
			Destroy(collider);
		}
	}
	

	// Visualización del rango de ataque en la vista de Scene
	void OnDrawGizmos()
	{
		Vector3 position = attackPoint.position;

		position.y += 0.25f;
		if (attackPoint == null)
			return;

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(position, attackRange);
	}
}
