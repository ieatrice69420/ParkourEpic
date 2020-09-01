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

    void OnEnable()
    {
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);
        controller.enabled = true;
        multiplayerBotStateManager.agent.enabled = false;
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
    }

    void OnDisable() => ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
}