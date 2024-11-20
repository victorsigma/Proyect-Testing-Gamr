using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLevel : MonoBehaviour
{
	[SerializeField]
	private string sound = "MapTraslation";
	// Start is called before the first frame update
	void Start()
	{
		AudioManager.instance.PlayMusic(sound);
	}
}
