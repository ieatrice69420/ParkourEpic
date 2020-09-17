using UnityEngine;
using TMPro;
using System;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
public class UiFriendprefab : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI friendnametext;

	[SerializeField] private FriendInfo friend;

	public static Action<string> Onremovedfriend = delegate { };

	public void initlaize(FriendInfo friend)
	{
		this.friend = friend;

		friendnametext.SetText(this.friend.UserId);
	}
	public void removerfriend()
	{
		Onremovedfriend?.Invoke(friend.UserId);
	}
}