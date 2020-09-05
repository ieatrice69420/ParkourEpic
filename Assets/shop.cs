using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class shop : MonoBehaviour
{

	#region Varibles
	public int money = 100;


	public TextMeshProUGUI moneytext;
	public TextMeshProUGUI Invetory;
	#endregion

	#region Unity Methods
	public void AddItem(string item)
	{
		Invetory.text += "/n" + item;
	}
	public void Update()
    {
	
	}

	#endregion
}