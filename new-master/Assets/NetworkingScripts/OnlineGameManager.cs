using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    public GameObject player;

    void Start()
    {
    	PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}
