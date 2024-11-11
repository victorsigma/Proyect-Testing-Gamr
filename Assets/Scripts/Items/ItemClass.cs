using System;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
	public string type; // "mele" o "shot"
	public int damage;
	public float range; // Solo aplicable si el tipo es "shot"
}

[System.Serializable]
public class Consumable
{
	public int healing;
	public int speed;
	public int duration;
}

[System.Serializable]
public class Equipment
{
	public int resistance; // Daño que puede reducir
}

[System.Serializable]
public class Item
{
	public string name;
	public string description;
	public Weapon wepon = null;
	public Consumable consumable = null;
	public Equipment equipment = null;
	public String sound = "";
}

[System.Serializable]
public class ItemDatabase
{
	public List<Item> items;
}
