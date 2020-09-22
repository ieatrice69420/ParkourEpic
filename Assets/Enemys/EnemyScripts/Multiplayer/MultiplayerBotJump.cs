using System.Collections;
using static System.Math;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.AI;

public class MultiplayerBotJump : BotClass
{
    [SerializeField]
    CharacterController controller;
    public Vector3 velocity;
    [SerializeField]
    float jumpHeight, gravity;
    public float actualJumpHeight { get; private set; }
    bool isGrounded = true;
    Vector3 jumpDir;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    bool touchingWall;
    [SerializeField]
    MultiplayerBotWallRun wallRun;
    bool stillTouchingWall;
    [SerializeField]
    MultiplayerBotRopeSwing rope;

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
        WallCheck();
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
                StartCoroutine(Jump(Vector3.Distance(multiplayerBotStateManager.data.startPos, multiplayerBotStateManager.data.endPos) / multiplayerBotStateManager.agent.speed, multiplayerBotStateManager.stats.jumpDelay + UnityEngine.Random.Range(multiplayerBotStateManager.stats.jumpDelayRangeMin, multiplayerBotStateManager.stats.jumpDelayRangeMax)));
                break;
            case TriggerState.WallJump:
                break;
            case TriggerState.WallRun:
                break;
        }
    }

    public IEnumerator Jump(float duration, float delay)
    {
        OffMeshLinkData data = multiplayerBotStateManager.agent.currentOffMeshLinkData;
        jumpDir = new Vector3((data.endPos - data.startPos).x, 0f, (data.endPos - data.startPos).z).normalized;
        isGrounded = false;
        multiplayerBotStateManager.agent.enabled = false;

        yield return new WaitForSeconds(Clamp(delay, 0f, .6f));

        if (controller.isGrounded) velocity.y = actualJumpHeight;

        yield return new WaitForSeconds(duration);

        while (!controller.isGrounded)
        {
            if (!enabled) break;

            isGrounded = true;
            multiplayerBotStateManager.agent.enabled = true;
            yield return null;
        }
    }

    void OnDisable()
    {
        ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
    }

    void WallCheck()
    {
        if (!controller.isGrounded && touchingWall)
        {
            wallRun.wallRunDir = jumpDir;
            wallRun.wallJumpSpeed = 0f;
            multiplayerBotStateManager.moveState = MoveState.WallRunning;
        }
    }

    IEnumerator OnCollisionEnter(Collision other)
    {
        yield return new WaitForSeconds
        (
            Mathf.Clamp
            (
                multiplayerBotStateManager.stats.wallRunDelay + Random.Range
                (
                    multiplayerBotStateManager.stats.wallRunDelayMinModifier,
                    multiplayerBotStateManager.stats.wallRunDelayMaxModifier
                ),
                0f,
                Mathf.Infinity
            )
        );

        if (stillTouchingWall) touchingWall = true;
    }

    private void OnCollisionStay() => stillTouchingWall = true;

    private void OnCollisionExit(Collision other) => stillTouchingWall = false;

    public void Roll() => multiplayerBotStateManager.moveState = MoveState.Rolling;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Rope"))
            if (!rope.isSwinging)
            {
                Rigidbody rb = hit.transform.parent.gameObject.GetComponent<Rigidbody>();
                Vector3 dir = rb.transform.position - transform.position;
                float fl = Mathf.Max(dir.x, dir.z);
                Vector3 pushDir = new Vector3(dir.x / Mathf.Abs(fl), 0f, dir.z / Mathf.Abs(fl));
                rb.AddForce(pushDir * rope.ropeSpeed);
                rope.rope = rb.transform;
                rope.hitOffset = hit.point - rope.rope.position;
                rope.rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
                multiplayerBotStateManager.moveState = MoveState.Roping;
            }
        if (hit.collider.CompareTag("Zipline")) multiplayerBotStateManager.moveState = MoveState.Ziplining;
    }
}