using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	[SerializeField]
	private GameObject teleportOutput;
	
	private GameObject player;
	private Animator animator; // Componente Animator para manejar las animaciones
	
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject;
		}
	}

	// Update is called once per frame
	void Update()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject;
		}
	}
	
	public void TeleportTo() 
	{
		animator.SetTrigger("Teleport");
	}
	
	
	public void TeleportToEnd() 
	{
		player.transform.position = teleportOutput.transform.position;
	}
}
