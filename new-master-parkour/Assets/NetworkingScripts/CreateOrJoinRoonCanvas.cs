using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoonCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu createRoomMenu;
    [SerializeField]
    private roomlistingsmenu _roomlistingsmenu;

    private RoomsCanveses _roomcanveses;
    public void FirstInitialize(RoomsCanveses Canveses)
    {
        _roomcanveses = Canveses;
        createRoomMenu.FirstInitialize(Canveses);
        _roomlistingsmenu.FirstInaitlize(Canveses);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
