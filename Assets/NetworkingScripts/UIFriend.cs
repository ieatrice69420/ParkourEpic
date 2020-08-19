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
        roomName = info.Room;
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
    public void JoinFriendRoom()
    {
        Debug.Log($"Joining Friend's Room: {info.Room}");
        if (string.IsNullOrEmpty(info.Room)) return;
        PhotonNetwork.JoinRoom(info.Room);
    }
}