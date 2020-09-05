using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unlocking : MonoBehaviour
{
	public InvetoryObject Invetory;

	public void OnTriggerEnter(Collider other)
	{
		var item = other.GetComponent<ItemScript>();
		if (item)
		{
			Invetory.AddItem(item.item, 1);
			Destroy(other.gameObject);

		}
	}
	private void Start()
	{
		foreach ( slot in Invetoryslot)
		{

		}
	}
	private void OnApplicationQuit()
	{
		Invetory.Container.Clear();
	}
}