using UnityEngine;

public class MultiplayerBotWallRun : BotClass
{
    [HideInInspector]
    public Vector3 wallRunDir;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float wallRunSpeed;
    public Vector3 velocity;

    void Update()
    {
        WallRun();
    }

    void WallRun()
    {
        controller.Move(wallRunDir * wallRunSpeed * Time.deltaTime);
    }
}