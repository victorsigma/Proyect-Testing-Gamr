using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Character")]
public class Characters : ScriptableObject
{
	public GameObject character;
	
	public Sprite characterImage;
	
	public RuntimeAnimatorController animatorController;
	
	public string characterName;
}
