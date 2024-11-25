using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanels : MonoBehaviour
{
	[SerializeField]
	private GameObject pcPanel;
	[SerializeField]
	private GameObject xboxPanel;
	[SerializeField]
	private GameObject movilPanel;

	void Start()
	{
		Time.timeScale = 0f;
		GameGlobals.uiStatus = "pause";
		switch (GameGlobals.lastInput)
		{
			case "keyboard":
				pcPanel.SetActive(true);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(false);
				break;
			case "joystick":
				pcPanel.SetActive(false);
				xboxPanel.SetActive(true);
				movilPanel.SetActive(false);
				break;
			case "touch":
				pcPanel.SetActive(false);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(true);
				break;
			default:
				pcPanel.SetActive(true);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(false);
				break;
		}
	}

	void Update()
	{
		switch (GameGlobals.lastInput)
		{
			case "keyboard":
				pcPanel.SetActive(true);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(false);
				break;
			case "joystick":
				pcPanel.SetActive(false);
				xboxPanel.SetActive(true);
				movilPanel.SetActive(false);
				break;
			case "touch":
				pcPanel.SetActive(false);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(true);
				break;
			default:
				pcPanel.SetActive(true);
				xboxPanel.SetActive(false);
				movilPanel.SetActive(false);
				break;
		}
	}
}
