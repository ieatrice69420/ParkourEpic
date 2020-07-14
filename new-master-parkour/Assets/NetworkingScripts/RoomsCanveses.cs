using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanveses : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoonCanvas _createOrJoinRoonCanvas;
    public CreateOrJoinRoonCanvas CreateOrJoinRoonCanvas { get { return _createOrJoinRoonCanvas; } }
    [SerializeField]

    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas currentRoomCanvas { get { return _currentRoomCanvas; } }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoonCanvas.FirstInitialize(this);
        currentRoomCanvas.FirstInitialize(this);
    }
}
