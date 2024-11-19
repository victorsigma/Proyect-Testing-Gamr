using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	public GameObject bulletPrefab; // Prefab del proyectil
	public float bulletSpeed = 10f; // Velocidad del proyectil
	public float shootDelay = 1f;   // Retraso entre disparos
	private float shootTimer = 0f;  // Temporizador para disparar
	private GameObject player;
	
	[SerializeField]
	private Vector3 shootPosition;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	void Update()
	{
		PlayerDetectorAIBox playerDetector = GetComponent<PlayerDetectorAIBox>();
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject;
		}

		// Disparar si el jugador es detectado
		if (playerDetector.PlayerDetected && player != null)
		{
			ShootAtPlayer();
		}

		// Reducir el temporizador de disparo
		if (shootTimer > 0f)
		{
			shootTimer -= Time.deltaTime;
		}
	}

	void ShootAtPlayer()
	{
		// Solo disparar si el temporizador ha terminado
		if (shootTimer <= 0f)
		{
			// Crear el proyectil
			Vector3 shootPoint = shootPosition + gameObject.transform.position; 
			GameObject bullet = Instantiate(bulletPrefab, shootPoint, Quaternion.identity);

			// Calcular la dirección hacia el jugador
			Vector2 shootDirection = (player.transform.position - shootPoint).normalized;

			// Asignar la dirección y velocidad al proyectil
			Bullet bulletScript = bullet.GetComponent<Bullet>();
			bulletScript?.SetDirection(shootDirection);

			// Reiniciar el temporizador de disparo
			shootTimer = shootDelay;
		}
	}
	
	private void OnDrawGizmos() 
	{
		Vector3 shootPoint = shootPosition + gameObject.transform.position; 
		
		Gizmos.color = Color.green;
		
		
		Gizmos.DrawWireSphere(shootPoint, 0.3f);
	}
}
