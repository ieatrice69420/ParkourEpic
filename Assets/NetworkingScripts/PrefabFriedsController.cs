using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab;
using System.Linq;
using System;

public class PrefabFriedsController : MonoBehaviour
{
	public static Action<List<FriendInfo>> onfriendlistupdated = delegate { };
	private List<FriendInfo> friends; 
	private void Awake()
	{
		PlayFabController.getphotonfriends += handlegetfriends;

		friends = new List<FriendInfo>();
		UiAddFriend.OnAddFriend += handleaddplayfabfriend;
		UiFriendprefab.Onremovedfriend += handleremovefriend;
	}

	private void handlegetfriends()
	{
		getplayfabfriends();
	}

	private void OnDestroy()
	{
		UiAddFriend.OnAddFriend -= handleaddplayfabfriend;
		UiFriendprefab.Onremovedfriend -= handleremovefriend;
		PlayFabController.getphotonfriends -= handlegetfriends;

	}

	private void handleremovefriend(string name)
	{
		string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
		var request = new RemoveFriendRequest { FriendPlayFabId = id };
		PlayFabClientAPI.RemoveFriend(request, onfriendremovedseccess, onfailure);
	}

	private void onfriendremovedseccess(RemoveFriendResult result)
	{
		getplayfabfriends();
	}

	private void handleaddplayfabfriend(string name)
	{
		var request = new AddFriendRequest { FriendTitleDisplayName = name };
		PlayFabClientAPI.AddFriend(request, onfriendaddedsucess, onfailure);
	}



	private void onfriendaddedsucess(AddFriendResult result)
	{
		getplayfabfriends();
	}

	public void getplayfabfriends()
	{
		var request = new GetFriendsListRequest { IncludeFacebookFriends = false, IncludeSteamFriends = false, XboxToken = null };
		PlayFabClientAPI.GetFriendsList(request,OnFriendslistSuccess , onfailure);
	}

	public void OnFriendslistSuccess(GetFriendsListResult result)
	{
		onfriendlistupdated?.Invoke(result.Friends);
		friends = result.Friends;
		Debug.Log("friend list updated");
	}

	private void onfailure(PlayFabError error)
	{
		Debug.Log($"error {error.GenerateErrorReport()}");
	}
}