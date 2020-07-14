using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public Button playButton;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOps = new RoomOptions();
        roomOps.IsVisible = true;
        roomOps.IsOpen = true;
        PhotonNetwork.NickName = "";
        string roomName = "Room" + Random.Range(0, 1000);
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
