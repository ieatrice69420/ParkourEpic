using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New inventory", menuName = "Invetory System/DataBase")]
public class ItemDataBaseObject : MonoBehaviour, ISerializationCallbackReceiver
{

	public ItemObject[] items;
	public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
	public void OnBeforeSerialize()
	{
		GetId = new Dictionary<ItemObject, int>();
		for (int i = 0; i < items.Length; i++)
		{
			GetId.Add(items[i], i);
		}
	}

	public void OnAfterDeserialize()
	{

	}
}