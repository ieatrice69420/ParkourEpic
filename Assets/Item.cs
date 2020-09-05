using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Item : MonoBehaviour
{

	#region Varibles
	public int cost;

	public shop shop;

	public string itemname;
	#endregion
	private void Start()
	{
		if (PlayerPrefs.HasKey("MONEY"))
		{
			shop.money = PlayerPrefs.GetInt("MONEY");
			shop.moneytext.text = PlayerPrefs.GetInt("MONEY").ToString();
		}
		else
		{
			shop.moneytext.text = shop.money.ToString();
		}
	}
	public void bought()
	{
		Debug.Log("Bought() has called");
		if (shop.money >= cost)
		{
			shop.money -= cost;
			shop.AddItem(itemname);
			PlayerPrefs.SetInt("MONEY", shop.money);
		}
		else
			Debug.Log("you do not have the money to buy this item");
	}
}