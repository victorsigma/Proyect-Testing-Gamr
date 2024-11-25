using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualButtons : MonoBehaviour
{
	// Start is called before the first frame update
	CanvasGroup canvasGroup;
	
	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameGlobals.lastInput == "touch")
		{
			canvasGroup.alpha =  1;
			canvasGroup.interactable = true;
		}
		else
		{
			canvasGroup.alpha =  0;
			canvasGroup.interactable = false;
		}
	}
}
