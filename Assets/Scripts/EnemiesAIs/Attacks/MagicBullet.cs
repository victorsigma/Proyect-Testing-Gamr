using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
	public float moveSpeed = 10f;  // Velocidad del proyectil
	public float lifetime = 5f;    // Tiempo en segundos antes de destruir el proyectil si no ha colisionado
	public int damage = 1;         // Daño del proyectil

	public Vector2 direction;     // Dirección del movimiento del proyectil

	void Start()
	{
		// Calcula la dirección del proyectil (debe ser asignada desde el enemigo o jugador que lo dispara)
		//direction = transform.up;  // Asumimos que el proyectil se mueve hacia adelante (en el eje Y o la dirección del frente del prefab)

		// Destruir el proyectil después de un tiempo
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		// Mover el proyectil
		Debug.Log("Direction: " + direction);

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			// Calcula la dirección hacia el jugador
			direction = (player.transform.position - transform.position).normalized;
			transform.Translate(direction * moveSpeed * Time.deltaTime);
		}
		//transform.Rotate(RotarZHaciaVector(direction));

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Aquí puedes definir lo que sucede al colisionar con algo
		// Como por ejemplo aplicar daño a un enemigo o jugador
		if (collision.gameObject.CompareTag("Player"))
		{
			// Suponiendo que el jugador tiene un componente PlayerController que recibe daño
			PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
			if (playerController != null)
			{
				playerController.SetDamage(damage);
			}

			// Destruir el proyectil después de la colisión
			Destroy(gameObject);
		}
		else
		{
			// Destruir el proyectil si choca con algo que no sea el jugador o un enemigo
			Debug.Log("hi");
			Destroy(gameObject);
		}
	}
}
