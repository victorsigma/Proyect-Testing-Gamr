using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 3f; // Velocidad de movimiento
	private Rigidbody2D rb; // Componente Rigidbody2D
	public Vector2 movement; // Vector de movimiento
	private Animator animator; // Componente Animator

	public Joystick joystick;

	private int life;

	[SerializeField]
	private LifeBar lifeBar;

	private bool isHit = false;

	private bool isDead = false;

	public delegate void EventHandler();
	public event EventHandler DeadPlayer;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		life = GameManager.instance.playerHealthCurrent;
		moveSpeed = GameManager.instance.playerSpeedCurrent;
		lifeBar.InitLifeBar(life);
	}

	void Update()
	{
		if (!isDead)
		{
			if (GameGlobals.lastInput == "touch")
			{
				MovimentTouch();
			}
			else
			{
				if (!GameGlobals.inventoryOn)
				{
					MovimentAll();
				}
				else
				{
					movement.x = 0;
					movement.y = 0;
				}
			}
		}
		else
		{
			movement.x = 0;
			movement.y = 0;
		}

		if (life != GameManager.instance.playerHealthCurrent)
		{
			life = GameManager.instance.playerHealthCurrent;
			lifeBar.ChangeCurrentLife(life);
		}
		
		if (moveSpeed != GameManager.instance.playerHealthCurrent)
		{
			moveSpeed = GameManager.instance.playerSpeedCurrent;
		}
	}

	public void SetDamage(Vector2 direction, int damage)
	{
		isHit = true;
		Vector2 push = (transform.position - (Vector3)direction).normalized;
		rb.AddForce(push, ForceMode2D.Impulse);

		// Modifica la vida global desde el GameManager
		GameManager.instance.ModifyPlayerHealth(-damage);
		life = GameManager.instance.playerHealthCurrent;
		lifeBar.ChangeCurrentLife(life);

		if (life <= 0 && !isDead)
		{
			isDead = true;
			DeadPlayer?.Invoke();
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			gameObject.layer = 0;
		}
	}


	void FixedUpdate()
	{
		// Mueve al personaje
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}


	void MovimentTouch()
	{
		// Captura la entrada del jugador
		float horizontal = joystick.Horizontal;
		float vertical = joystick.Vertical;

		ProcessOppositeKeys(ref horizontal, ref vertical);
		ProcessMovement(ref horizontal, ref vertical);

		// Actualiza las animaciones
		UpdateAnimation();
	}

	void MovimentAll()
	{
		// Captura la entrada del jugador
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");

		ProcessOppositeKeys(ref moveX, ref moveY);
		ProcessMovement(ref moveX, ref moveY);

		// Actualiza las animaciones
		UpdateAnimation();
	}



	void ProcessOppositeKeys(ref float moveX, ref float moveY)
	{
		bool isOppositeX = (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow));
		bool isOppositeY = (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow));

		if (isOppositeX)
		{
			moveX = 0;
		}
		if (isOppositeY)
		{
			moveY = 0;
		}
	}

	void ProcessMovement(ref float moveX, ref float moveY)
	{
		if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
		{
			movement.x = moveX;
			movement.y = 0;
		}
		else
		{
			movement.x = 0;
			movement.y = moveY;
		}
	}



	void UpdateAnimation()
	{
		// Configura las animaciones según la dirección del movimiento
		if (movement != Vector2.zero)
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
