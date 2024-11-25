using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorial : MonoBehaviour
{
	private float previousRightTriggerValue = 0.0f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Leer el valor de Right Trigger (RT)
		float rightTriggerValue = Input.GetAxis("RightTrigger");

		bool rightTriggerPulse = previousRightTriggerValue <= 0.6 && rightTriggerValue >= 0.7;

		// Actualizar el estado anterior del Right Trigger para usarlo en el próximo frame
		previousRightTriggerValue = rightTriggerValue;
		if (GameGlobals.lastInput != "touch")
		{
			if (Input.GetButtonDown("FireButton") || rightTriggerPulse)
			{
				UseItem();
			}
		}
	}


	public void UseItem()
	{
		Time.timeScale = 1f;
		GameGlobals.uiStatus = "none";
		gameObject.SetActive(false);
	}
}
