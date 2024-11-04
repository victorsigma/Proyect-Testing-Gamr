using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		int indexCharacter = PlayerPrefs.GetInt("CharacterIndex");
		Instantiate(GameManager.instance.characters[indexCharacter].character, transform.position, Quaternion.identity);
	}
}
