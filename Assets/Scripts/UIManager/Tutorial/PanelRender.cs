using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRender : MonoBehaviour
{
	[SerializeField]
	private bool isRender = false;
	
	[SerializeField]
	private GameObject panel;

	// Update is called once per frame
	void Update()
	{
		PlayerDetectorAIBox playerDetector = GetComponent<PlayerDetectorAIBox>();
		
		if(playerDetector.PlayerDetected && !isRender) 
		{
			panel.SetActive(true);
			isRender = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		
		{
			panel.SetActive(true);
			if(gameObject.CompareTag("Item")) 
			{
				Destroy(gameObject);
			}
			isRender = true;
		}
	}
}
