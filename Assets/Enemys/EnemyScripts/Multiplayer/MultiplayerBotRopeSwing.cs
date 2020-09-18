﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerBotRopeSwing : BotClass
{
    public Transform rope { get; set; }
    public bool isSwinging;
    [SerializeField]
    float speed, maxJumpSpeed;
    public float ropeSpeed;
    public float jumpSpeed;
    public Transform ropeTip;
    public Vector3 ropeNewPos;
    bool alreadyCalledOnSwing;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    [SerializeField]
    Vector3 ropeOldPos;
    public Vector3 hitOffset { get; set; }
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    NavMeshAgent agent;
    List<Transform> ropeTips = new List<Transform>();
    Vector3 velocity;

    private void OnEnable()
    {
        agent.enabled = false;
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);

        StartCoroutine(CallUpdate());
        isSwinging = true;
    }

    IEnumerator CallUpdate()
    {
        while (enabled)
        {
            RunAlways();
            yield return null;
        }
    }

    private void Start()
    {
        ropeTips = RopeManager.ropeTips;
    }

    void RunAlways()
    {
        Debug.Log("AAAAAAAAA");

        Debug.Log(isSwinging);
        if (isSwinging)
        {
            Swing();
            if (!alreadyCalledOnSwing) StartCoroutine(OnSwing());
            controller.enabled = false;
        }
        else
        {
            alreadyCalledOnSwing = false;
            controller.Move((ropeNewPos * jumpSpeed + velocity) * Time.deltaTime);
        }

        if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Jumping;
        Debug.Log("AAAAAAAAA");
    }

    void Swing() => transform.position = rope.GetChild(0).GetChild(0).position;

    void OnTriggerEnter()
    {
        if (jumpSpeed <= maxJumpSpeed - 5f) jumpSpeed = 0f;
    }

    void FixedUpdate()
    {
        if (isSwinging)
        {
            ropeNewPos = rope.GetChild(0).position - ropeOldPos;
            ropeOldPos = rope.GetChild(0).position;
        }
    }

    IEnumerator OnSwing()
    {
        alreadyCalledOnSwing = true;

        yield return new WaitForSeconds
        (
/*             Mathf.Clamp
            (
                multiplayerBotStateManager.stats.ropeJumpDelay + Random.Range
                (
                    multiplayerBotStateManager.stats.ropeJumpDelayMinModifier,
                    multiplayerBotStateManager.stats.ropeJumpDelayMaxModifier
                ),
                0f,
                Mathf.Infinity
            ) */
            0.2f
        );

        Detach();
    }

    void Detach()
    {
        isSwinging = false;
        jumpSpeed = maxJumpSpeed;
        rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        controller.enabled = true;
    }
}