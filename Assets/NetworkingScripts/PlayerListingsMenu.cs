using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform content;
    [SerializeField]
    private PlayerListing _playerListing;

    List<PlayerListing> _listing = new List<PlayerListing>();


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListing listing = Instantiate(_playerListing, content);
        if(listing != null)
        {
            listing.SetPlayerInfo(newPlayer);
            _listing.Add(listing);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listing.FindIndex(x => x._Player == otherPlayer);
        if(index != -1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }
    }
}