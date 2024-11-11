using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBarButton : MonoBehaviour
{
	[SerializeField]
	private GameObject view;
	// Update is called once per frame
	void Update()
	{
		if(view.activeInHierarchy) 
		{
			gameObject.GetComponent<Button>().interactable = false;
		} else 
		{
			gameObject.GetComponent<Button>().interactable = true;
		}
	}
}
