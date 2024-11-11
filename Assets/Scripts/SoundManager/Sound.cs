using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSound", menuName = "Sound")]public class Sound : ScriptableObject
{
	public String nameTrack;
	public AudioClip clip;
}