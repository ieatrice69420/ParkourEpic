using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeaveRoomMenu : MonoBehaviourPunCallbacks
{
    public RoomsCanveses roomsCanveses;

    public void FirstInitialize(RoomsCanveses canveses)
    {
        roomsCanveses = canveses;
    }
    public void OnclickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        roomsCanveses.currentRoomCanvas.Hide();

    }

}
