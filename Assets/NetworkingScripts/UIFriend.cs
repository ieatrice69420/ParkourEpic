using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class UIFriend : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI friendNameText;
    [SerializeField] public string roomName;
    [SerializeField] private FriendInfo info;
    [SerializeField] private Image onlineImage;
    [SerializeField] private Color offlineColor;
    [SerializeField] private Color onlineColor;

    public void Initialize(FriendInfo friendInfo)
    {
        info = friendInfo;
        roomName = $"{info.UserId}";
        friendNameText.text = $"{info.UserId}";

        if (info.IsOnline)
        {
            onlineImage.color = onlineColor;
        }
        else
        {
            onlineImage.color = offlineColor;
        }
    }
    public Player Player { get; private set; }

    public TextMeshProUGUI playerinroomtext;
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        playerinroomtext.text = player.NickName;
        Debug.Log($"Player Joined {player.NickName}");
    }

    public void JoinFriendRoom()
    {
        Debug.Log($"Joining Friend's Room: {roomName}");
        if (string.IsNullOrEmpty(info.Room)) return;
        PhotonNetwork.JoinRoom(roomName);
    }
}