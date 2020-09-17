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
			Debug.Log("Item to collect has found");
			Invetory.AddItem(item.item, 1);
			Destroy(other.gameObject);

		}
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Invetory.Save();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Invetory.Load();
		}
	}
	private void OnApplicationQuit()
	{
		Invetory.Container.Clear();
	}
}