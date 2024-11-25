using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTutorial : MonoBehaviour
{
	[SerializeField]
	private Joystick joystick;

	void Update()
	{
		switch (GameGlobals.lastInput)
		{
			case "keyboard":
				MovimentAll();
				break;
			case "joystick":
				MovimentAll();
				break;
			case "touch":
				MovimentTouch();
				break;
			default:
				MovimentAll();
				break;
		}
	}

	void MovimentTouch()
	{
		// Captura la entrada del jugador
		float horizontal = joystick.Horizontal;
		float vertical = joystick.Vertical;
		
		if(horizontal != 0 || vertical != 0) 
		{
			Time.timeScale = 1f;
			GameGlobals.uiStatus = "none";
			gameObject.SetActive(false);
		}
	}

	void MovimentAll()
	{
		// Captura la entrada del jugador
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		
		
		if(moveX != 0 || moveY != 0 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) 
		{
			Time.timeScale = 1f;
			GameGlobals.uiStatus = "none";
			gameObject.SetActive(false);
		}
	}
}
