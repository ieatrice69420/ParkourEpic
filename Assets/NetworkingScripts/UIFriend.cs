using UnityEngine;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab.ClientModels;

public class UIFriend : MonoBehaviour
{
    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        Debug.Log($"Player Joined {player.NickName}");
    }


    [SerializeField] public TextMeshProUGUI friendNameText;
    [SerializeField] public string roomName;
    [SerializeField] private PlayFab.ClientModels.FriendInfo info;



    [SerializeField] private Image onlineImage;
    [SerializeField] private Color offlineColor;
    [SerializeField] private Color onlineColor;
    public void Initialize(PlayFab.ClientModels.FriendInfo friendInfo)
    {
        info = friendInfo;
        //roomName = $"{friendInfo.UserId}";
        friendNameText.text = $"{info.UserId}";
        /*
        if (friendInfo.IsOnline)
        {
            onlineImage.color = onlineColor;
        }
        else
        {
            onlineImage.color = offlineColor;
        }
        */
    }
    public void JoinFriendRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log($"Joining Friend's Room: {roomName}");
            PhotonNetwork.JoinRoom(roomName);
        }
        else
            Debug.Log("not connected so can't join");
    }


    public void SetPlayerInfoo(Player player)
    {
        Player = player;
        Debug.Log($"Player Joined {player.NickName}");
    }

}