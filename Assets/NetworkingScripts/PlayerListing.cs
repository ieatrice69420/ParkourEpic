using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListing : MonoBehaviour
{

    public Player _Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        player = _Player; 
    }


}
