using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorial : MonoBehaviour
{
	void Update()
	{
		if (GameGlobals.lastInput != "touch")
		{
			if (Input.GetButtonDown("Inventory"))
			{
				UseItem();
			}
		}
	}


	public void UseItem()
	{
		Time.timeScale = 1f;
		GameGlobals.uiStatus = "none";
		GameObject player = GameObject.FindWithTag("Player");
		player?.GetComponent<Inventory>()?.OpenClose();
		gameObject.SetActive(false);
	}
}
