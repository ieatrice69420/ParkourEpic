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
    }

    void Update()
    {
        WallRun();
        if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Jumping;
    }

    void WallRun()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(((wallRunDir * wallRunSpeed) + velocity) * Time.deltaTime);

        if (FindClosest(wallJumpManager.wallJumpTriggers).sqrDistance <= minDis)
        {
            WallJump();
        }
    }

    void WallJump()
    {

    }

    void OnCollisionExit()
    {
        wallRunSpeed = 0;
    }

    void OnDisable() => ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
}