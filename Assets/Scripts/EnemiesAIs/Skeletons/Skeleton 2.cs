using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton2 : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Vector2 directionDamage = new Vector2(transform.position.x, 0);

			collision.gameObject.GetComponent<PlayerController>().SetDamage(directionDamage, 1);
		}
	}
}
