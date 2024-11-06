using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject player;
	
	
	public void EndAnimation() 
	{
		gameObject.SetActive(false);
		player.GetComponent<PlayerMeleeAttack>().isAttacking = false;
	}
	
	public void DestroyItem() 
	{
		player.GetComponent<Inventory>().DestroyItem();
	}
}
