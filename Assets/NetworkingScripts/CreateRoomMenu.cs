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
    public RoomsCanveses _roomcanveses;
    [SerializeField]
    private PlayFabController PlayFabController;


    public void FirstInitialize(RoomsCanveses Canveses)
    {
        _roomcanveses = Canveses;
    }


    private void Awake()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.PublishUserId = true;
        PhotonNetwork.JoinOrCreateRoom(result.DisplayName, options, TypedLobby.Default);
        Debug.Log(PlayerPrefs.GetString("USERNAME"));
        Debug.Log("Created room successfuly", this);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room creation failed" + message, this);

    }
}
