using UnityEngine;

public class MultiplayerBotWallRun : MonoBehaviour
{
    [HideInInspector]
    public Vector3 wallRunDir;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float wallRunSpeed;

    void Update()
    {
        WallRun();
    }

    void WallRun()
    {
        controller.Move(wallRunDir * wallRunSpeed * Time.deltaTime);
    }
}