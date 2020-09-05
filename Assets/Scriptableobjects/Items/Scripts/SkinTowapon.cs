using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SkinTowapon", menuName = "Inventory/system/Items/SkinTowapon")]
public class SkinTowapon : ItemObject
{
	public void Awake()
	{
		type = itemtype.weaponskins;
	}
}