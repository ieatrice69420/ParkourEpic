﻿using UnityEngine;

public class MultiplayerBotStateManager : MonoBehaviour
{
    #region Enums

    public enum MoveState
    {
        Jumping = 0,
        WallRunning = 1,
        Camping = 2
    }

    public enum PathFindState
    {
        Wandering = 0,
        Following = 1,
        Finding = 2
    }

    public enum ShootState
    {
        Idle = 0,
        Spraying = 1,
        Precise = 2
    }

    #endregion

    #region States

    public MoveState moveState = MoveState.Jumping;

    public PathFindState pathFindState = PathFindState.Wandering;

    public ShootState shootState = ShootState.Idle;

    #endregion

    [SerializeField]
    Behaviour[] moveScripts, pathFindScripts, shootScripts;
    public Vector3 desiredPosition;

    void Update()
    {
        for (int i = 0; i < moveScripts.Length - 1; i++)
        {
            if (moveScripts[i] != null)
            {
                if (moveScripts[i] == null)
                {
                    Debug.LogError(moveScripts[i] + "doesnt exist!");
                    break;
                }
                moveScripts[i].enabled = i == (int)moveState;
            }
        }

        for (int i = 0; i < pathFindScripts.Length - 1; i++)
        {
            if (pathFindScripts[i] == null)
            {
                Debug.LogError(pathFindScripts[i] + "doesnt exist!");
                break;
            }
            pathFindScripts[i].enabled = i == (int)pathFindState;
        }

        for (int i = 0; i < shootScripts.Length - 1; i++)
        {
            if (shootScripts[i] == null)
            {
                Debug.LogError(shootScripts[i] + "doesnt exist!");
                break;
            }
            shootScripts[i].enabled = i == (int)shootState;
        }
    }
}