using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using PlayFab.ClientModels;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    public UpdateUserTitleDisplayNameResult result;

    [SerializeField]
    private PlayFabController PlayFabController;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.PublishUserId = true;
        PhotonNetwork.CreateRoom(PlayerPrefs.GetString("USERNAME"), options, TypedLobby.Default);
        Debug.Log(PlayerPrefs.GetString("USERNAME"));
        Debug.Log("Created room successfuly", this);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room creation failed" + message, this);

    }
}
