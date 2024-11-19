using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{
	[SerializeField]
	private LifeBar lifeBar;
	void Start()
	{
		Enemy enemy = GetComponent<Enemy>();
		lifeBar.InitLifeBar(enemy.life, enemy.maxLife);
	}
	
	public void ChangeCurrentLife(int life) 
	{
		lifeBar.ChangeCurrentLife(life);
	}
}
