using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTutorial : MonoBehaviour
{
	[SerializeField]
	private GameObject teleportEnd;
	void Update()
	{
		if (GameGlobals.lastInput != "touch")
		{
			if (Input.GetButtonDown("Confirm"))
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
		if (GameGlobals.lastInput == "touch")
		{
			player?.GetComponent<PlayerController>()?.UseTeleport();
		}
		teleportEnd.SetActive(true);
		gameObject.SetActive(false);
	}
}
