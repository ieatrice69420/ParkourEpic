using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class roomlistingsmenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    public Roomlisting roomListing;

    private List<Roomlisting> listings = new List<Roomlisting>();

    public RoomsCanveses RoomsCanveses;


    public void FirstInaitlize(RoomsCanveses canveses)
    {
        RoomsCanveses = canveses;
    }

    public override void OnJoinedRoom()
    {
        RoomsCanveses.currentRoomCanvas.Show();
        content.DestroyChildren();
        listings.Clear();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
            }
            //added to room list
            else
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index == -1)
                {
                    Roomlisting listing = Instantiate(roomListing, content);
                    if (listing != null)
                    {

                        listing.SetRoomInfo(info);
                        listings.Add(listing);
                    }
                }
                
            }
        }
    }
}
