using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum itemtype
{
	skins,
	weaponskins,
	knife,
	Deffult
}
public abstract class ItemObject : ScriptableObject
{
	public GameObject prefab;
	public itemtype type;
	[TextArea(15, 20)]
	public string description;
}