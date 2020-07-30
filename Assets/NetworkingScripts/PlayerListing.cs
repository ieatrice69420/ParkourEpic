using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    public Player Player { get; private set; }

    public bool Ready = false;
    public void SetPlayerInfo(Player player)
    {
        _text.text = PlayerPrefs.GetString("USERNAME");
        player = Player;
    }

}
