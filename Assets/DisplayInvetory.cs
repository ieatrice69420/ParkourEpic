using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DisplayInvetory : MonoBehaviour
{
	public InvetoryObject invetory;

	public int X_SPACE_BETWEEN_ITEM;
	public int NUMBER_OF_COlOMN;
	public int Y_SPACE_BETWEEN_ITEM;

	Dictionary<Invetoryslot, GameObject> itemdisplyed = new Dictionary<Invetoryslot, GameObject>();
	public void UpdateDisplay() 
	{
		for (int i = 0; i < invetory.Container.Count; i++)
		{
			if (itemdisplyed.ContainsKey(invetory.Container[i]))
			{
				itemdisplyed[invetory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
			}
			else
			{
				var obj = Instantiate(invetory.Container[i].item.prefab, Vector3.zero, Quaternion.identity);
				Debug.Log("g");
				obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
				obj.GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
				itemdisplyed.Add(invetory.Container[i], obj);
			}
		}
	}
	void Start()
	{

		createDisplay();
	}
	private void Update()
	{
		UpdateDisplay();
	}
	public void createDisplay()
	{
		for (int i = 0; i < invetory.Container.Count; i++)
		{
			var obj = Instantiate(invetory.Container[i].item.prefab, Vector3.zero,Quaternion.identity);
			Debug.Log("g");
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
			obj.GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
			itemdisplyed.Add(invetory.Container[i], obj);

		}
	}

	public Vector3 GetPosition(int i)
	{
		return new Vector3(X_SPACE_BETWEEN_ITEM * (i %NUMBER_OF_COlOMN), (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COlOMN)), 0f);
	}
}