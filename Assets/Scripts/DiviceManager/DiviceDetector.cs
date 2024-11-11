using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiviceDetector : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float leftTriggerValue = Input.GetAxis("LeftTrigger");
		float rightTriggerValue = Input.GetAxis("RightTrigger");

		bool leftTriggerPulse = leftTriggerValue > 0.2;
		bool rightTriggerPulse = rightTriggerValue > 0.2;

		float rightStickX = Input.GetAxis("JoystickAxis4");
		float rightStickY = Input.GetAxis("JoystickAxis5");

		Vector3 stickDirection = new Vector3(rightStickX, rightStickY, 0f);

		if (Input.anyKeyDown && Input.touchCount == 0)
		{
			if (DetectKeyboardInput())
			{
				GameGlobals.lastInput = "keyboard";
			}
			else if (DetectMouseInput())
			{
				GameGlobals.lastInput = "keyboard";
			}
			else
			{
				GameGlobals.lastInput = "joystick";
			}
		}

		// Detectar si se está utilizando el joystick
		if (leftTriggerPulse || rightTriggerPulse || stickDirection.magnitude > 0.1f)
		{
			GameGlobals.lastInput = "joystick";
		}

		// Detectar si se ha tocado la pantalla
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0); // Detectar el primer toque

			if (touch.phase == TouchPhase.Began) // Si el toque ha comenzado
			{
				GameGlobals.lastInput = "touch";
			}
		}

		// Detectar si se oprime algún botón del mando
		if (DetectGamepadButtons())
		{
			GameGlobals.lastInput = "joystick";
		}
	}

	bool DetectKeyboardInput()
	{
		foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(keyCode))
			{
				if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z || keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9 || keyCode == KeyCode.Space)
				{
					return true;
				}
			}
		}
		return false;
	}

	bool DetectMouseInput()
	{
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				return true;
			}
		}
		return false;
	}

	// Detectar botones del mando
	bool DetectGamepadButtons()
	{
		// Detectar botones del mando por mapeo manual (valores Button0, Button1, etc.)
		if (Input.GetKeyDown(KeyCode.JoystickButton0) || // Botón A (X en PS)
			Input.GetKeyDown(KeyCode.JoystickButton1) || // Botón B (Círculo en PS)
			Input.GetKeyDown(KeyCode.JoystickButton2) || // Botón X (Cuadrado en PS)
			Input.GetKeyDown(KeyCode.JoystickButton3))   // Botón Y (Triángulo en PS)
		{
			return true;
		}

		return false;
	}
}
