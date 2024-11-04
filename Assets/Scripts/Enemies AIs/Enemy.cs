using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int life = 10;
	
	private Animator animator;
	
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	public void TakeDamage(int damage)
	{
		life-=damage;
		animator.SetBool("isHit", true);
		
		if(life <= 0) 
		{
			Destroy(gameObject);
		}
	}
	
	public void SetNotHit()
	{
		animator.SetBool("isHit", false);
	}
}
