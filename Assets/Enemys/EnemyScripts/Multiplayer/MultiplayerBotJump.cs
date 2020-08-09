using System.Collections;
using static System.Math;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.AI;

public class MultiplayerBotJump : BotClass
{
    // [SerializeField]
    // multiplayerBotStateManager.agent multiplayerBotStateManager.agent;
    [SerializeField]
    CharacterController controller;
    public Vector3 velocity;
    [SerializeField]
    float jumpHeight, gravity;
    float actualJumpHeight;
    bool isGrounded = true;
    Vector3 jumpDir;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;

    void Start() => actualJumpHeight = (float)Sqrt((double)(jumpHeight * -2f * gravity));

    void OnEnable()
    {
        isGrounded = true;
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);
    }

    void LateUpdate()
    {
        JumpCheck();
        Move();
        Gravity();
    }

    void Move()
    {
        if (isGrounded)
        {
            if (multiplayerBotStateManager.agent.enabled == true && multiplayerBotStateManager.agent.isOnNavMesh) multiplayerBotStateManager.agent.SetDestination(multiplayerBotStateManager.desiredPosition);
        }
        else
        {
            controller.Move(jumpDir * multiplayerBotStateManager.agent.speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded) velocity.y = -2f;
    }

    void JumpCheck()
    {
        switch (multiplayerBotStateManager.triggerState)
        {
            case TriggerState.Jump:
                StartCoroutine(Jump(Vector3.Distance(multiplayerBotStateManager.data.startPos, multiplayerBotStateManager.data.endPos) / multiplayerBotStateManager.agent.speed, multiplayerBotStateManager.stats.jumpDelay + Random.Range(multiplayerBotStateManager.stats.jumpDelayRangeMin, multiplayerBotStateManager.stats.jumpDelayRangeMax)));
                break;
            case TriggerState.WallJump:
                break;
            case TriggerState.WallRun:
                break;
        }
    }

    public IEnumerator Jump(float duration, float delay)
    {
        Debug.Log("f");
        OffMeshLinkData data = multiplayerBotStateManager.agent.currentOffMeshLinkData;
        jumpDir = new Vector3((data.endPos - data.startPos).normalized.x, 0f, (data.endPos - data.startPos).normalized.z);
        isGrounded = false;
        multiplayerBotStateManager.agent.enabled = false;
        yield return new WaitForSeconds(Clamp(delay, 0f, .6f));
        if (controller.isGrounded) velocity.y = actualJumpHeight;
        yield return new WaitForSeconds(duration);
        while (true)
        {
            if (controller.isGrounded)
            {
                isGrounded = true;
                multiplayerBotStateManager.agent.enabled = true;
                break;
            }
            yield return null;
        }
    }

    void OnDisable()
    {
        ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
    }
}