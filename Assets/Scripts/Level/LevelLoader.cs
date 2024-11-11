using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	[SerializeField]
	private string level;
	
	public void LoadLevel() 
	{
		GameManager.instance.LoadScene(level);
	}
}
