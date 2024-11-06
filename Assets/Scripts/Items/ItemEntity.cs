using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
	// Tiempo en segundos antes de que el objeto se destruya automáticamente
	private float lifeTime = 12f;
	public bool isPickedUp = false;

	// Start is called before the first frame update
	void Start()
	{
		// Destruir el objeto después de un tiempo
		Destroy(gameObject, lifeTime);
	}
}
