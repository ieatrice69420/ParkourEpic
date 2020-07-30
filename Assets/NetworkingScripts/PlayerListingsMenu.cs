using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    public PlayerListing PlayerListing; 

    private List<PlayerListing> listings = new List<PlayerListing>();


    public int PlayersInRoom;
    //private bool _ready = false;
    private void Awake()
    {
        getcurrentroomplayers();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //SetReadyUp(false); 
    }

    public override void OnLeftRoom()
    {
        content.DestroyChildren();
    }

    private void getcurrentroomplayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        foreach (KeyValuePair <int,Player> PlayerInfo in PhotonNetwork.CurrentRoom.Players)
        {

            AddPlayerListing(PlayerInfo.Value);
            PlayersInRoom++;
        }

    }

    public void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(PlayerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            listings.Add(listing);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }


    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
        //roomsCanveses.currentRoomCanvas.LeaveRoomMenu.OnclickLeaveRoom();
    //}
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }

    public void OnclickStartTheGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(2);
        }
    }
}
