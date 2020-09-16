using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerBotRope : BotClass
{
    public Transform rope { get; set; }
    public bool isSwinging { get; set; }
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

    private void OnEnable()
    {
        agent.enabled = false;
        isSwinging = true;
    }

    private IEnumerator Start()
    {
        ropeTips = RopeManager.ropeTips;
        while (true)
        {
            RunAlways();
            yield return null;
        }
    }

    void RunAlways()
    {
        Debug.Log("AAAAAAAAA");
        rope = FindClosest(ropeTips).transform;

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
            controller.Move(ropeNewPos * jumpSpeed * Time.deltaTime);
        }

        if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Jumping;
        Debug.Log("AAAAAAAAA");
    }

    void Swing() => transform.position = rope.position;

    void OnTriggerEnter()
    {
        if (jumpSpeed <= maxJumpSpeed - 5f) jumpSpeed = 0f;
    }

    void FixedUpdate()
    {
        if (isSwinging)
        {
            ropeNewPos = rope.position - ropeOldPos;
            ropeOldPos = rope.position;
        }
    }

    IEnumerator OnSwing()
    {
        alreadyCalledOnSwing = true;

        yield return new WaitForSeconds
        (
            Mathf.Clamp
            (
                multiplayerBotStateManager.stats.ropeJumpDelay + Random.Range
                (
                    multiplayerBotStateManager.stats.ropeJumpDelayMinModifier,
                    multiplayerBotStateManager.stats.ropeJumpDelayMaxModifier
                ),
                0f,
                Mathf.Infinity
            )
        );

        Detach();
    }

    void Detach()
    {
        jumpSpeed = maxJumpSpeed;
        rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        isSwinging = false;
        controller.enabled = true;
    }
}