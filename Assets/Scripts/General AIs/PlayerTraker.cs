using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraker : MonoBehaviour
{
	// Start is called before the first frame update

	private Transform player;
	void Start()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
		else
		{
			Debug.LogWarning("No se encontró un objeto con el tag 'Player'");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (player != null)
		{
			gameObject.transform.position = player.position;
		}
		else
		{
			GameObject playerObject = GameObject.FindWithTag("Player");
			if (playerObject != null)
			{
				player = playerObject.transform;
			}
			else
			{
				Debug.LogWarning("No se encontró un objeto con el tag 'Player'");
			}
		}
	}

	void OnDrawGizmos()
	{
		Vector3 position = gameObject.transform.position;

		position.y -= 0.004f;

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(position, 0.2f);
	}
}
