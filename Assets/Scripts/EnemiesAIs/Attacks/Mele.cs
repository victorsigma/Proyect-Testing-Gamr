using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : MonoBehaviour
{
	[SerializeField]
	private int damage = 2;
	
	void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			Vector2 directionDamage = new Vector2(transform.position.x, 0);
			
			collision.gameObject.GetComponent<PlayerController>().SetDamage(directionDamage, damage);
		}
	}
}
