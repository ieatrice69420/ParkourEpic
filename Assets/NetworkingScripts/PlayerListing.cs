using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform content;
    [SerializeField]
    public UiFriendRoom _uifriendroom;

    private List<UiFriendRoom> _Uifriend = new List<UiFriendRoom>();

    private void AddPlayerListing(Player player)
    {
        UiFriendRoom uIFriend = Instantiate(_uifriendroom, content);
        if (uIFriend != null)
        {
            uIFriend.SetPlayerInfo(player);
            _Uifriend.Add(uIFriend);
            Debug.Log("OnPlayerEnteredRoom was called");
        }
    }

    private void GetCurrentRoomPlayers()
    {
        Debug.Log("Trying to get the current room's players");
        Debug.Log($"Are we in a room though: {PhotonNetwork.InRoom}");
        if (!PhotonNetwork.InRoom) return;
        foreach (KeyValuePair<int, Player> PlayerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("OnPlayerEnteredRoom was called");
            AddPlayerListing(PlayerInfo.Value);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        Debug.Log("OnPlayerEnteredRoom was called");

    }
    public override void OnJoinedRoom()
    {
        GetCurrentRoomPlayers();
    }



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _Uifriend.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_Uifriend[index].gameObject);
            _Uifriend.RemoveAt(index);
            Debug.Log("why did you left the room?");
        }
    }



    public override void OnLeftRoom()
    {
        Debug.Log("left Room");

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
    }



}