using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustumproportygenerator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    private ExitGames.Client.Photon.Hashtable _mycustumproporty = new ExitGames.Client.Photon.Hashtable();
    private void setcustumnumber()

    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(1, 99);

        _text.text = result.ToString();

        _mycustumproporty["RandomNumber"] = result; 
        PhotonNetwork.LocalPlayer.CustomProperties = _mycustumproporty;
    }



    public void onClick_Button()
    {
        setcustumnumber();
    }
}
