using UnityEngine;
using UnityEngine.AI;

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
    WallJumpManager wallJumpManager;
    [SerializeField]
    float minDis;
    public float wallJumpSpeed;
    public Vector3 wallJumpDir;
    bool isWallJumping;
    bool isWallRunning;
    public Collision other;
    [SerializeField]
    MultiplayerBotJump jump;

    void Awake() => defaultSpeed = wallRunSpeed;

    private void Start()
    {
        wallJumpManager = WallJumpManager.instance;
        minDis *= minDis;
    }
    void OnEnable()
    {
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);
        controller.enabled = true;
        multiplayerBotStateManager.agent.enabled = false;
        wallRunSpeed = defaultSpeed;
        isWallRunning = true;
    }

    void Update()
    {
        if (isWallRunning) WallRun();

        // if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Jumping;

        controller.Move(((wallRunDir * wallRunSpeed) + velocity) * Time.deltaTime);
    }

    void WallRun()
    {
        velocity.y += gravity * Time.deltaTime;

        if (FindClosest(wallJumpManager.wallJumpTriggers).sqrDistance <= minDis)
        {
            wallJumpSpeed = 7f;
            velocity.y = jump.actualJumpHeight;

            #region WallJumpVectorCalc

            wallJumpDir = Vector3.Reflect(wallRunDir, other.GetContact(0).normal).normalized;

            #endregion

            isWallJumping = true;
            isWallRunning = false;
        }

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

    void OnCollisionExit()
    {
        wallRunSpeed = 0;
        isWallRunning = true;
    }

    void OnDisable() => ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
}