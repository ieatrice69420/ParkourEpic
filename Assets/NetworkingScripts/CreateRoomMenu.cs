using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI _roomname;

    public RoomsCanveses _roomcanveses;

    public void FirstInitialize(RoomsCanveses Canveses)
    {
        _roomcanveses = Canveses;
    }

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.PublishUserId = true;
        PhotonNetwork.JoinOrCreateRoom(_roomname.text, options, TypedLobby.Default);


        Debug.Log("Created room successfuly", this);

        _roomcanveses.currentRoomCanvas.Show();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room creation failed" + message, this);

    }
}
