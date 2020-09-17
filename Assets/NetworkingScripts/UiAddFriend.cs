using UnityEngine;
using System;
public class UiAddFriend : MonoBehaviour
{

	#region Varibles
	[SerializeField] private string Displayname;

	public static Action<string> OnAddFriend = delegate { };
	#endregion

	public void SetAddFriendName(string name)
	{
		Displayname = name;
	}

	public void AddFriend()
	{
		if (string.IsNullOrEmpty(Displayname)) return;
		OnAddFriend?.Invoke(Displayname);
	}
}