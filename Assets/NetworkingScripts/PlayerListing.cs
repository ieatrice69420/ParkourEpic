using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform content;
    [SerializeField]
    private UIFriend _uifriend;

    private List<UIFriend> _Uifriend = new List<UIFriend>();

    public void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            print("Yes, connected!");
        }

        if (PhotonNetwork.InRoom)
        {
            print("Yes, I am in a room!");
        }
        else
        {
            print("No, not in a room.");
        }
        GetCurrentRoomPlayers();

    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> PlayerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(PlayerInfo.Value);
            Debug.Log("OnPlayerEnteredRoom was called");
        }
    }
    private void AddPlayerListing(Player player)
    {
        UIFriend uIFriend = Instantiate(_uifriend, content);
        if (uIFriend != null)
        {
            uIFriend.SetPlayerInfo(player);
            _Uifriend.Add(uIFriend);
            Debug.Log("OnPlayerEnteredRoom was called");

        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        Debug.Log("OnPlayerEnteredRoom was called");

    }



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _Uifriend.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_Uifriend[index].gameObject);
            _Uifriend.RemoveAt(index);
        }
    }



    public override void OnLeftRoom()
    {
        Debug.Log("left Room");

    }

}