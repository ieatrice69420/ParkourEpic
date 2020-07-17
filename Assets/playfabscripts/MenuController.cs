using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

	#region Varibles
	public static MenuController Mc;


	public GameObject[] buttonLocks;

	public Button[] unlockedbuttons;
	#endregion

	private void OnEnable()
	{
		Mc = this;
	}
	private void Start()
	{
		SetUpStore();
	}
	public void SetUpStore()
	{
		for (int i = 0; i < Prescictentdtata.PD.allskins.Length; i++)
		{
			buttonLocks[i].SetActive(!Prescictentdtata.PD.allskins[i]);
			unlockedbuttons[i].interactable = Prescictentdtata.PD.allskins[i];
		}
	}

	public void UnlockedSkin(int index)
	{
		Prescictentdtata.PD.allskins[index] = true;
		PlayFabController.PFC.SetUserData(Prescictentdtata.PD.SkinsDataToString());
		SetUpStore();
	}



	public void SetMySkin(int whichskin)
	{
		Prescictentdtata.PD.myskin = whichskin;
	}
}