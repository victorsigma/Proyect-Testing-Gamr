using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualUiButtons : MonoBehaviour
{
	Button button;
	// Start is called before the first frame update
	void Start()
	{
		button = GetComponent<Button>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameGlobals.lastInput == "touch")
		{
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
		}
	}
}
