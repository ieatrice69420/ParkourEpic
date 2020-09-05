using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Deffult Object", menuName = "Inventory/system/Items/Default")]
public class DeffultObject : ItemObject
{
	public void Awake()
	{
		type = itemtype.Deffult;
	}
}