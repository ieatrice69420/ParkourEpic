using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New inventory",menuName = "Invetory System/Invetory")]
public class InvetoryObject : ScriptableObject, ISerializationCallbackReceiver
{
	public string savepath;
	public List<Invetoryslot> Container = new List<Invetoryslot>();
	public ItemDataBaseObject ItemDataBaseObject;
	public void AddItem(ItemObject _item, int _amount)
	{
		for (int i = 0; i < Container.Count; i++)
		{
			if(Container[i].item == _item)
			{
				Container[i].AddAmount(_amount);
				return;
			}
		}
		Container.Add(new Invetoryslot(_item, _amount, ItemDataBaseObject.GetId[_item]));
	}

	public void Save()
	{
		string savedata = JsonUtility.ToJson(this, true);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(string.Concat(Application.persistentDataPath, savepath));
		bf.Serialize(file, savedata);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(string.Concat(Application.persistentDataPath, savepath))){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
			JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
			file.Close();
		}
	}
	public void OnAfterDeserialize()
	{

		for (int i = 0; i < Container.Count; i++)
		{
			Container[i].item = ItemDataBaseObject.Getitem[Container[i].ID];
		}
	}

	public void OnBeforeSerialize()
	{
	}
}
[System.Serializable]
public class Invetoryslot
{
	public ItemObject item;

	public int amount;
	public int ID;

	public Invetoryslot(ItemObject _item, int _amount, int _ID)
	{
		_ID = ID;
		item = _item;
		amount = _amount;
	}
	public void AddAmount(int value)
	{
		amount += value;
	}
}