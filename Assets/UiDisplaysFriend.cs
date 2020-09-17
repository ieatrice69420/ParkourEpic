using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using System;

public class UiDisplaysFriend : MonoBehaviour
{
	[SerializeField] private Transform friendcontainer;
	[SerializeField] private UiFriendprefab UiFriendprefab;

	private void Awake()
	{
		GetPhotonFriendController.ondisplayfriends += handledisplayfriends;
	}
	private void OnDestroy()
	{
		GetPhotonFriendController.ondisplayfriends -= handledisplayfriends;
	}

	private void handledisplayfriends(List<FriendInfo> friends)
	{
		foreach (Transform child in friendcontainer)
		{
			Destroy(child);
			Debug.Log("cleared the friend's playfab");
		}

		foreach (FriendInfo friend in friends)
		{
			UiFriendprefab uIFriend = Instantiate(UiFriendprefab, friendcontainer);
			uIFriend.initlaize(friend);
			Debug.Log("a");

		}
	}
}