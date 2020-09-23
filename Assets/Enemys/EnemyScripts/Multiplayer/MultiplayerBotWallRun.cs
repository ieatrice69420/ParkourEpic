using UnityEngine;
using UnityEngine.AI;
using System.Linq.Move;

public class MultiplayerBotWallRun : BotClass
{
    public Vector3 wallRunDir;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float wallRunSpeed;
    public Vector3 velocity;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    [SerializeField]
    float gravity;
    float defaultSpeed;
    [SerializeField]
    WallJumpManager wallJumpManager;
    [SerializeField]
    float minDis;
    public float wallJumpSpeed;
    public Vector3 wallJumpDir;
    bool isWallJumping;
    bool isWallRunning;
    [SerializeField]
    MultiplayerBotJump jump;
    [SerializeField]
    MultiplayerWayPoints wayPoints;

    void Awake() => defaultSpeed = wallRunSpeed;

    private void Start()
    {
        WallJumpManager tempManager = WallJumpManager.instance;
        if (tempManager != null) wallJumpManager = tempManager;
        minDis *= minDis;
    }
    void OnEnable()
    {
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);
        controller.enabled = true;
        multiplayerBotStateManager.agent.enabled = false;
        // Destroy(multiplayerBotStateManager.agent);
        wallRunSpeed = defaultSpeed;
        isWallRunning = true;
        wayPoints.wayPointList.Move(wayPoints.disabledWayPointList, FindClosest(wayPoints.wayPointList).closest);
    }

    void Update()
    {
        if (isWallRunning) WallRun();

        if (controller.isGrounded)
        {
            velocity.y = -2f;
            multiplayerBotStateManager.moveState = MoveState.Jumping;
        }
        else controller.Move(velocity * Time.deltaTime);
    }

    void WallRun()
    {
        velocity.y += gravity * Time.deltaTime;

        if (isWallJumping)
        {
            WallJump();
        }
    }

    void WallJump()
    {
        controller.Move(wallJumpDir * wallJumpSpeed * Time.deltaTime);

        if (wallJumpSpeed > 0f) wallJumpSpeed -= 4f * Time.deltaTime;
        else
        {
            wallJumpSpeed = 0f;
            isWallJumping = false;
        }
    }

    private void OnCollisionEnter()
    {
        wallRunDir = transform.forward;
        isWallRunning = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (FindClosest(wallJumpManager.wallJumpTriggers).sqrDistance <= minDis || Input.GetKey(KeyCode.P))
        {
            wallJumpSpeed = 7f;

            #region WallJumpVectorCalc

            wallJumpDir = Vector3.Reflect(wallRunDir, other.GetContact(0).normal).normalized;

            velocity = new Vector3
            (
                wallJumpDir.x,
                jump.actualJumpHeight,
                wallJumpDir.z
            );

            #endregion

            isWallJumping = true;
            isWallRunning = false;
        }
    }

    void OnCollisionExit()
    {
        wallRunSpeed = 0;
        isWallRunning = true;
    }

    void OnDisable()
    {
        ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
        multiplayerBotStateManager.agent.enabled = true;
        // multiplayerBotStateManager.pathFindState = PathFindState.Objective;
    }
}