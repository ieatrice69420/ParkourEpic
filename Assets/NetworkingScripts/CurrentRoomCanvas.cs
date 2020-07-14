using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomsCanveses _roomcanveses;

    [SerializeField]
    private PlayerListingsMenu playerListingsMenu;
    [SerializeField]
    private LeaveRoomMenu leaveRoomMenu;

    public LeaveRoomMenu LeaveRoomMenu { get { return leaveRoomMenu; } }
    public void Show()
    {
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    } 
    public void FirstInitialize(RoomsCanveses Canveses)
    {
        _roomcanveses = Canveses;
        playerListingsMenu.FirstInitialize(Canveses);
        leaveRoomMenu.FirstInitialize(Canveses);
    }
}
