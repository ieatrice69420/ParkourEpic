using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New inventory",menuName ="Invetory/Invetory")]
public class InvetoryObject : ScriptableObject
{
public List<Invetoryslot> Container = new List<Invetoryslot>();
	public void AddItem(ItemObject _item, int _amount)
	{
		bool hasitem = false;
		for (int i = 0; i < Container.Count; i++)
		{
			if(Container[i].item == _item)
			{
				Container[i].AddAmount(_amount);
				hasitem = true;
				break;
			}
		}
		if (!hasitem)
		{
			Container.Add(new Invetoryslot(_item, _amount));


		}
	}
}
[System.Serializable]
public class Invetoryslot
{
	public ItemObject item;

	public int amount;

	public Invetoryslot(ItemObject _item, int _amount)
	{
		item = _item;
		amount = _amount;
	}
	public void AddAmount(int value)
	{
		amount += value;
	}
}