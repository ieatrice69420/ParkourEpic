using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using playfabfriendinfo = PlayFab.ClientModels.FriendInfo;
using photonfriendinfo = Photon.Realtime.FriendInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GetPhotonFriendController : MonoBehaviourPunCallbacks
{
	public static Action<List<photonfriendinfo>> ondisplayfriends = delegate { };
	private void Awake()
	{
		PrefabFriedsController.onfriendlistupdated += handlefriendupdated;
	}

	private void OnDestroy()
	{
		PrefabFriedsController.onfriendlistupdated -= handlefriendupdated;
	}

	private void handlefriendupdated(List<playfabfriendinfo> friends)
	{
		if(friends.Count != 0)
		{
			string[] friendDisplayname = friends.Select(f => f.TitleDisplayName).ToArray();
			PhotonNetwork.FindFriends(friendDisplayname);
		}
	}
	public override void OnFriendListUpdate(List<photonfriendinfo> friendList)
	{
		ondisplayfriends?.Invoke(friendList);
		Debug.Log("friend list updated(photon)");
	}
}