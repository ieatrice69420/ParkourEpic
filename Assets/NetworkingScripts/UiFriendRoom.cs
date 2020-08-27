using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;

public class UiFriendRoom : MonoBehaviour
{
    public Player Player { get; private set; }

    public TextMeshProUGUI playerinroomtext;
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        playerinroomtext.text = PlayerPrefs.GetString("USERNAME");
        playerinroomtext.text = player.NickName;
        Debug.Log($"Player Joined {player.NickName}");
    }
}