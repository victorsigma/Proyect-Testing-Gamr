using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
	[SerializeField]
	private GameObject thanks;
	
	void Update()
	{
		if(Input.GetButtonDown("Cancel")) 
		{
			GameManager.instance.LoadScene("MapTraslation");
		}
	}
	
	public void Skip() 
	{
		GameManager.instance.LoadScene("MapTraslation");
	}
	
	public void ShowEnd() 
	{
		thanks.SetActive(true);
		gameObject.SetActive(false);
	}
}
