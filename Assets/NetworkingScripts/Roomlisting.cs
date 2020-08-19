using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Roomlisting : MonoBehaviour
{
    public RoomInfo RoomInfo {get;private set;}
    public void SetRoomInfo(RoomInfo roominfo)
    {
        RoomInfo = roominfo;
    }

    public void OnClick_Button() {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
