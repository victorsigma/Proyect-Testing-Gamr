using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipButtons : MonoBehaviour
{
	[SerializeField]
	private GameObject pcTip;
	[SerializeField]
	private GameObject xboxTip;
	void Start()
	{
		switch (GameGlobals.lastInput)
		{
			case "keyboard":
				pcTip.SetActive(true);
				xboxTip.SetActive(false);
				break;
			case "joystick":
				pcTip.SetActive(false);
				xboxTip.SetActive(true);
				break;
			case "touch":
				pcTip.SetActive(false);
				xboxTip.SetActive(false);
				break;
			default:
				pcTip.SetActive(true);
				xboxTip.SetActive(false);
				break;
		}
	}

	void Update()
	{
		switch (GameGlobals.lastInput)
		{
			case "keyboard":
				pcTip.SetActive(true);
				xboxTip.SetActive(false);
				break;
			case "joystick":
				pcTip.SetActive(false);
				xboxTip.SetActive(true);
				break;
			case "touch":
				pcTip.SetActive(false);
				xboxTip.SetActive(false);
				break;
			default:
				pcTip.SetActive(true);
				xboxTip.SetActive(false);
				break;
		}
	}
}
