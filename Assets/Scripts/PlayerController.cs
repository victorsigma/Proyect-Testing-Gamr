using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 3f; // Velocidad de movimiento
	private Rigidbody2D rb; // Componente Rigidbody2D
	public Vector2 movement; // Vector de movimiento
	private Animator animator; // Componente Animator

	[SerializeField]
	private Joystick joystick;

	private int life;

	[SerializeField]
	private LifeBar lifeBar;

	private bool isHit = false;

	private bool isDead = false;

	public delegate void EventHandler();
	public event EventHandler DeadPlayer;

	[SerializeField]
	public bool isInTeleport = false;
	
	[SerializeField]
	public bool isLevelLoader = false;

	[SerializeField]
	private GameObject teleport;
	
	[SerializeField]
	private GameObject levelLoader;

	[SerializeField]
	private GameObject interactButton;

	[SerializeField]
	private TextMeshProUGUI textMeshProTips;

	[SerializeField]
	private Image imageKey;

	[SerializeField]
	private Sprite xboxKey;

	[SerializeField]
	private Sprite pcKey;

	[SerializeField]
	private GameObject keyTips;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		life = GameManager.instance.playerHealthCurrent;
		moveSpeed = GameManager.instance.playerSpeedCurrent;
		lifeBar.InitLifeBar(life);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Teleport")
		{
			interactButton.SetActive(true);
			if (GameGlobals.lastInput == "keyboard" || GameGlobals.lastInput == "joystick")
			{
				keyTips.SetActive(true);
			}

			teleport = collider.gameObject;
			isInTeleport = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Teleport")
		{
			interactButton.SetActive(false);
			keyTips.SetActive(false);
			teleport = null;
			isInTeleport = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "LevelLoader")
		{
			interactButton.SetActive(true);
			if (GameGlobals.lastInput == "keyboard" || GameGlobals.lastInput == "joystick")
			{
				keyTips.SetActive(true);
			}
			levelLoader = collision.gameObject;
			isLevelLoader = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "LevelLoader")
		{
			interactButton.SetActive(false);
			keyTips.SetActive(false);
			levelLoader = null;
			isLevelLoader = false;
		}
	}

	void Update()
	{
		if (!isDead)
		{
			if (GameGlobals.lastInput == "touch")
			{
				keyTips.SetActive(false);
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

		if (moveSpeed != GameManager.instance.playerSpeedCurrent)
		{
			moveSpeed = GameManager.instance.playerSpeedCurrent;
		}

		Interact();
	}

	public void UseTeleport()
	{
		teleport.GetComponent<Teleport>().TeleportTo();
	}
	
	public void LoadLevel()
	{
		levelLoader.GetComponent<LevelLoader>().LoadLevel();
	}

	public void Interact()
	{
		if (GameGlobals.lastInput == "keyboard")
		{
			imageKey.sprite = pcKey;
		}
		else if (GameGlobals.lastInput == "joystick")
		{
			imageKey.sprite = xboxKey;
		}
		if (isInTeleport)
		{
			if (Input.GetButtonDown("Confirm"))
			{
				UseTeleport();
			}
		}
		
		if(isLevelLoader) 
		{
			if (Input.GetButtonDown("Confirm"))
			{
				LoadLevel();
			}
		}
	}
	public void InteractMobile()
	{
		if (isInTeleport)
		{
			UseTeleport();
		}
		if(isLevelLoader) 
		{
			LoadLevel();
		}
	}

	public void SetDamage(Vector2 direction, int damage)
	{
		if (!isHit)
		{
			isHit = true;
			AudioManager.instance.PlaySFX("DamageMeet");
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
			else
			{
				animator.SetBool("isHit", true);
			}
		}
	}
	
	public void SetDamage(int damage)
	{
		if (!isHit)
		{
			isHit = true;
			AudioManager.instance.PlaySFX("DamageMeet");

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
			else
			{
				animator.SetBool("isHit", true);
			}
		}
	}

	public void SetNotHit()
	{
		isHit = false;
		animator.SetBool("isHit", false);
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
