using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Skins", menuName = "Inventory/system/Items/Skins")]
public class Skins : ItemObject
{
	public void Awake()
	{
		type = itemtype.weaponskins;
	}
}