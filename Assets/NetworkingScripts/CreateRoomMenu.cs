using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using PlayFab.ClientModels;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayFabController PlayFabController;

    string gameVersion = "1";


    public GameObject loobyCanvas;
    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            options.PublishUserId = true;
            PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("USERNAME"), options, TypedLobby.Default);
            Debug.Log(PlayerPrefs.GetString("USERNAME"));
            Debug.Log("Created room successfuly", this);
            loobyCanvas.SetActive(true);
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room creation failed" + message, this);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;

    }
}
