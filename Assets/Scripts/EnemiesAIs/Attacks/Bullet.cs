using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float moveSpeed = 10f;  // Velocidad del proyectil
	public float lifetime = 5f;    // Tiempo en segundos antes de destruir el proyectil si no ha colisionado
	public int damage = 1;         // Daño del proyectil
	private Vector2 direction;     // Dirección del movimiento del proyectil
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// Destruir el proyectil después de un tiempo
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate()
	{
		// Mover el proyectil usando el Rigidbody2D
		rb.velocity = (direction) * moveSpeed;
	}

	public void SetDirection(Vector2 newDirection)
	{
		direction = newDirection;

		// Calcular el ángulo en radianes y convertirlo a grados
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Ajustar el ángulo para el sprite con rotación inicial de 45 grados
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 133.9f));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
			if (playerController != null)
			{
				playerController.SetDamage(damage);
			}
			Destroy(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("HandWeapon"))
		{
			Destroy(gameObject);
		}
	}
}
