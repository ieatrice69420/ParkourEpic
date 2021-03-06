﻿using UnityEngine;
using UnityEngine.AI;
using System;

public class MultiplayerBotStateManager : BotClass
{
    #region States

    public MoveState moveState = MoveState.Jumping;

    public PathFindState pathFindState = PathFindState.Wandering;

    public ShootState shootState = ShootState.Idle;

    #endregion

    [SerializeField]
    Behaviour[] moveScripts, pathFindScripts, shootScripts;

    public Vector3 desiredPosition;
    public MultiplayerBotStats stats;

    public NavMeshAgent agent;

    public TriggerState triggerState;

    [HideInInspector]
    public OffMeshLinkData data;

    public Vector3 velocity;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float groundDistance = 0.1f;
    [SerializeField]
    MultiplayerWayPoints wayPoints;
    [SerializeField]
    MultiplayerBotFollow follow;
    public Transform heading;

    void Update()
    {
        if (moveState == MoveState.Jumping) data = agent.currentOffMeshLinkData;

        triggerState = SetTriggerState();

        if (moveState != MoveState.Ziplining && moveState != MoveState.Roping)
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
        }
        else if (moveState == MoveState.Roping)
        {
            foreach (Behaviour b in moveScripts) b.enabled = false;
            moveScripts[5].enabled = true;
        }
        else if (moveState == MoveState.Ziplining)
        {
            foreach (Behaviour b in moveScripts) b.enabled = false;
            moveScripts[4].enabled = true;
        }

        for (int i = 0; i < pathFindScripts.Length - 1; i++)
        {
            if (pathFindScripts[i] == null)
            {
                // Debug.LogError(pathFindScripts[i] + "doesnt exist!");
                break;
            }
            pathFindScripts[i].enabled = i == (int)pathFindState;
        }

        for (int i = 0; i < shootScripts.Length - 1; i++)
        {
            if (shootScripts[i] == null)
            {
                // Debug.LogError(shootScripts[i] + "doesnt exist!");
                break;
            }
            shootScripts[i].enabled = i == (int)shootState;
        }

        if (wayPoints.wayPointList.Count < 2)
        {
            pathFindState = PathFindState.Objective;
        }

        if (pathFindState != PathFindState.Following) CheckForVisiblePlayers();

        if (agent.enabled) heading.LookAt(transform.position + agent.velocity);
    }

    TriggerState SetTriggerState()
    {
        if (data.valid) return TriggerState.Jump;
        return TriggerState.WallRun;
    }

    void CheckForVisiblePlayers()
    {
        Transform[] targets = PlayersInSight(stats.fieldOfView, transform);
        ClosestObject closest = FindClosest(targets);

        if (closest.closest != null)
        {
            Transform closestPlayer = closest.closest.transform;

            if (closestPlayer != null)
            {
                pathFindState = PathFindState.Following;
                follow.target = closestPlayer;
            }
        }
    }
}