using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkAI : MonoBehaviour
{
	public float moveSpeed = 3f; // Velocidad de movimiento
	public float movementRadius = 5f; // Radio dentro del cual la IA puede moverse
	public float directionChangeDelay = 0.2f; // Tiempo entre cambios de dirección
	public float movementDelay = 0.2f; // Retraso antes de moverse en la nueva dirección
	private Vector2 initialPosition; // La posición inicial
	private Animator animator; // Referencia al Animator
	private Vector2 movement; // Para guardar la dirección del movimiento
	private float directionChangeTimer = 0f; // Temporizador para cambiar de dirección
	private float movementDelayTimer = 0f; // Temporizador para el retraso antes de moverse
	private bool isMoving = false; // Indica si la IA está en movimiento
	private bool directionChosen = false; // Indica si ya se ha escogido una dirección

	void Start()
	{
		animator = GetComponent<Animator>();
		initialPosition = transform.position; // Guardar la posición inicial
		ChooseNewDirection(); // Escoger una dirección al inicio
	}

	void Update()
	{
		
	}
	
	void FixedUpdate() 
	{
		PlayerDetectorAIBox playerDetector = GetComponent<PlayerDetectorAIBox>();

		if (!playerDetector.PlayerDetected)
		{
			// Si el temporizador de cambio de dirección ha terminado, escoger nueva dirección
			if (directionChangeTimer <= 0f && !directionChosen)
			{
				ChooseNewDirection();
			}
			else if (!directionChosen)
			{
				directionChangeTimer -= Time.deltaTime;
			}
			// Si se ha elegido una dirección, esperar el retraso antes de moverse
			if (directionChosen && movementDelayTimer <= 0f)
			{
				// Verificar si el movimiento sigue dentro del radio permitido
				Vector2 newPosition = (Vector2)transform.position + movement * moveSpeed * Time.deltaTime;

				// Comprobar si el nuevo movimiento está dentro del radio
				if (Vector2.Distance(initialPosition, newPosition) <= movementRadius)
				{
					transform.position = newPosition;
					isMoving = true;
				}
				else
				{
					isMoving = false; // Si no se mueve, cambiar el estado a no mover
				}

				// Al terminar el movimiento, reiniciamos para escoger una nueva dirección
				directionChangeTimer -= Time.deltaTime;
				if (directionChangeTimer <= 0f)
				{
					directionChosen = false;
				}
			}
			else if (directionChosen)
			{
				// Esperar antes de movernos
				movementDelayTimer -= Time.deltaTime;
			}

			// Actualizar las animaciones
			UpdateAnimation();
		} else 
		{
			directionChangeTimer = directionChangeDelay; // Reiniciar el temporizador de cambio de dirección
			movementDelayTimer = movementDelay; // Establecer el temporizador de retraso antes de moverse
			directionChosen = false; // Marcar que se ha escogido una nueva dirección
			initialPosition = transform.position;
			isMoving = false; // Aún no estamos moviéndonos hasta que termine el retraso
			//UpdateAnimation();
		}
	}

	// Escoger una dirección aleatoria
	void ChooseNewDirection()
	{
		// Elegir una dirección al azar: arriba, abajo, izquierda, derecha
		int direction = Random.Range(0, 4);
		switch (direction)
		{
			case 0: // Moverse hacia arriba
				movement = new Vector2(0, 1);
				break;
			case 1: // Moverse hacia abajo
				movement = new Vector2(0, -1);
				break;
			case 2: // Moverse hacia la izquierda
				movement = new Vector2(-1, 0);
				break;
			case 3: // Moverse hacia la derecha
				movement = new Vector2(1, 0);
				break;
		}

		directionChangeTimer = directionChangeDelay; // Reiniciar el temporizador de cambio de dirección
		movementDelayTimer = movementDelay; // Establecer el temporizador de retraso antes de moverse
		directionChosen = true; // Marcar que se ha escogido una nueva dirección
		isMoving = false; // Aún no estamos moviéndonos hasta que termine el retraso
	}

	void UpdateAnimation()
	{
		// Si hay movimiento, actualiza la animación
		if (isMoving && movement != Vector2.zero)
		{
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
			animator.SetBool("isMoving", true);
		}
		else
		{
			// Si no se mueve, detener la animación
			animator.SetBool("isMoving", false);
		}
	}
}
