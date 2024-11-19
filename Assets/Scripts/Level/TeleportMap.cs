using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMap : MonoBehaviour
{
	private Animator animator; // Componente Animator para manejar las animaciones
	
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	public void TeleportTo() 
	{
		animator.SetTrigger("Teleport");
	}
	
	
	public void TeleportToEnd() 
	{
		GameManager.instance.LoadScene("MapTraslation");
	}
}
