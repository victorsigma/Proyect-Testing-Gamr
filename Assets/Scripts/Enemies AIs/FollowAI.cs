using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowIA : MonoBehaviour
{
	public float moveSpeed = 5f; // Velocidad de movimiento
	[SerializeField]
	private Transform player;
	private Animator animator; // Referencia al Animator
	private Vector2 movement; // Para guardar la dirección del movimiento
	private float directionChangeDelay = 0.1f; // Tiempo de pausa al cambiar de dirección
	private float directionChangeTimer = 0f; // Temporizador para la pausa
	private bool isMoving = false; // Indica si la IA está en movimiento
	private bool prioritizeHorizontal = true; // Indica si la IA debe priorizar moverse en horizontal primero

	void Start()
	{
		animator = GetComponent<Animator>();
		// Encuentra el jugador por su etiqueta
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
		else
		{
			Debug.LogWarning("No se encontró un objeto con el tag 'Player'");
		}
	}

	void Update()
	{
	}
	
	void FixedUpdate() 
	{
		PlayerDetectorAIBox playerDetector = GetComponent<PlayerDetectorAIBox>();

		if (playerDetector.PlayerDetected)
		{
			Vector2 targetPosition = player.position;
			Vector2 currentPosition = transform.position;

			// Calcular la dirección de movimiento
			float horizontalDifference = targetPosition.x - currentPosition.x;
			float verticalDifference = targetPosition.y - currentPosition.y;

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
			// Llamamos a la función de actualización de animaciones
			UpdateAnimation();
		}
		else
		{
			movement = Vector2.zero; // No hay movimiento si el jugador no es detectado
			isMoving = false; // Detener el movimiento
		}
	}

	void UpdateAnimation()
	{
		// Configura las animaciones según la dirección del movimiento
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
}
