using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New inventory", menuName = "Invetory System/DataBase")]
public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{

	public ItemObject[] items;
	public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
	public Dictionary<int, ItemObject> Getitem = new Dictionary<int, ItemObject>();
	public void OnBeforeSerialize()
	{

	}

	public void OnAfterDeserialize()
	{
		GetId = new Dictionary<ItemObject, int>();
		Getitem = new Dictionary<int, ItemObject>();
		for (int i = 0; i < items.Length; i++)
		{
			Getitem.Add(i, items[i]);
			GetId.Add(items[i], i);
		}
	}
}