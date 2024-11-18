using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithDistanceIA : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float safeDistance = 5f; // Distancia mínima para detenerse
	public float distanceTolerance = 0.1f;
	[SerializeField]
	private Transform player;
	private Animator animator;
	private Vector2 movement;
	private float directionChangeDelay = 0.1f;
	private float directionChangeTimer = 0f;
	private bool isMoving = false;
	private bool prioritizeHorizontal = true;

	void Start()
	{
		animator = GetComponent<Animator>();
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
	}

	void Update()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
	}

	void FixedUpdate()
	{
		PlayerDetectorAIBox playerDetector = GetComponent<PlayerDetectorAIBox>();

		if (playerDetector.PlayerDetected)
		{
			Vector2 currentPosition = transform.position;
			float distanceToPlayer = Vector2.Distance(currentPosition, player.position);

			if (distanceToPlayer > safeDistance + distanceTolerance)
			{
				MoveTowardsPlayer(currentPosition, player.position);
			}
			else if (distanceToPlayer < safeDistance - distanceTolerance)
			{
				MoveAwayFromPlayer(currentPosition, player.position);
			}
			else
			{
				StopMovement(); // Detener el movimiento si está dentro del rango de tolerancia
			}

			// Actualizar las animaciones
			UpdateAnimation();
		}
		else
		{
			StopMovement(); // Detener movimiento si no se detecta al jugador
		}
	}

	void MoveTowardsPlayer(Vector2 currentPosition, Vector2 targetPosition)
	{
		// Calcular la diferencia en las posiciones
		float horizontalDifference = targetPosition.x - currentPosition.x;
		float verticalDifference = targetPosition.y - currentPosition.y;

		Vector2 newMovement = CalculateMovement(horizontalDifference, verticalDifference);

		// Si la dirección ha cambiado, reiniciar el temporizador
		if (newMovement != movement)
		{
			movement = newMovement;
			directionChangeTimer = directionChangeDelay; // Reiniciar el temporizador
			isMoving = false; // Detener el movimiento temporalmente
		}

		// Si el temporizador está en 0, mover la IA
		if (directionChangeTimer <= 0f)
		{
			isMoving = true;
			transform.position = Vector2.MoveTowards(currentPosition, currentPosition + movement, moveSpeed * Time.deltaTime);
		}
		else
		{
			directionChangeTimer -= Time.deltaTime; // Decrementar el temporizador
		}
	}

	void MoveAwayFromPlayer(Vector2 currentPosition, Vector2 targetPosition)
	{
		// Calcular las diferencias en las posiciones X y Y
		float horizontalDifference = currentPosition.x - targetPosition.x;
		float verticalDifference = currentPosition.y - targetPosition.y;

		// Calcular el movimiento en un solo eje (horizontal o vertical)
		Vector2 newMovement = Vector2.zero;

		// Priorizar moverse en un eje primero (horizontal o vertical)
		if (Mathf.Abs(horizontalDifference) > Mathf.Abs(verticalDifference))
		{
			// Si la diferencia horizontal es mayor, moverse solo horizontalmente
			newMovement = new Vector2(horizontalDifference > 0 ? 1 : -1, 0);
		}
		else
		{
			// Si la diferencia vertical es mayor, moverse solo verticalmente
			newMovement = new Vector2(0, verticalDifference > 0 ? 1 : -1);
		}

		// Si la dirección ha cambiado, reiniciar el temporizador
		if (newMovement != movement)
		{
			movement = newMovement;
			directionChangeTimer = directionChangeDelay; // Reiniciar el temporizador
			isMoving = false; // Detener el movimiento temporalmente
		}

		// Si el temporizador está en 0, mover la IA
		if (directionChangeTimer <= 0f)
		{
			transform.position = Vector2.MoveTowards(currentPosition, currentPosition + movement, moveSpeed * Time.deltaTime);
			isMoving = true;
		}
		else
		{
			directionChangeTimer -= Time.deltaTime; // Decrementar el temporizador
		}

		// Actualizar las animaciones
		UpdateAnimation();
	}


	Vector2 CalculateMovement(float horizontalDifference, float verticalDifference)
	{
		Vector2 newMovement;

		// Priorizar moverse en un eje primero (horizontal o vertical)
		if (prioritizeHorizontal)
		{
			if (Mathf.Abs(horizontalDifference) > 0.1f) // Moverse horizontalmente si no está alineado
			{
				newMovement = new Vector2(horizontalDifference > 0 ? 1 : -1, 0);
			}
			else
			{
				prioritizeHorizontal = false; // Cambiar a movimiento vertical cuando esté alineado
				newMovement = new Vector2(0, verticalDifference > 0 ? 1 : -1);
			}
		}
		else // Priorizar movimiento vertical después de alinearse horizontalmente
		{
			if (Mathf.Abs(verticalDifference) > 0.1f) // Moverse verticalmente si no está alineado
			{
				newMovement = new Vector2(0, verticalDifference > 0 ? 1 : -1);
			}
			else
			{
				prioritizeHorizontal = true; // Cambiar a movimiento horizontal cuando esté alineado
				newMovement = new Vector2(horizontalDifference > 0 ? 1 : -1, 0);
			}
		}

		return newMovement;
	}

	void UpdateAnimation()
	{
		if (isMoving && movement != Vector2.zero)
		{
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
			animator.SetBool("isMoving", true);
		}
		else
		{
			animator.SetBool("isMoving", false);
		}
	}

	void StopMovement()
	{
		movement = Vector2.zero;
		isMoving = false;
	}
}
