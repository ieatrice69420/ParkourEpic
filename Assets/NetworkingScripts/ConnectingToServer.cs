using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class ConnectingToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.Gamaeversion;
        print("Connectting to server");
        AuthenticationValues authenticationValues = new AuthenticationValues("0");
        PhotonNetwork.AuthValues = authenticationValues;
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.LocalPlayer.NickName);
        print("Connsected to server");
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        PhotonNetwork.FindFriends(new string[] { "1" });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server becasue " + cause.ToString());
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        base.OnFriendListUpdate(friendList);

        foreach (FriendInfo info in friendList)
        {
            Debug.Log("Friend info recived" + info.UserId + "Is online?" + info.IsOnline);
        }
    }
}
