using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Roomlisting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private PlayFabController PlayFabController;

    public RoomInfo RoomInfo {get;private set;}
    public void SetRoomInfo(RoomInfo roominfo)
    {
        RoomInfo = roominfo;
        //text.text = GetComponent<PlayFabController>().username;
    }

    public void OnClick_Button() {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
