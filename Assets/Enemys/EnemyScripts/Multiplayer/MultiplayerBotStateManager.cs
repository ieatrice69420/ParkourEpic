using UnityEngine;

public class MultiplayerBotStateManager : MonoBehaviour
{
    [SerializeField]
    MultiplayerBotJump multiplayerBotJump;

    #region Enums

	public enum MoveState
    {
        Jumping = 0,
        WallRunning = 1,
        Camping = 2
    }

    public enum PathFindState
    {
        Wandering = 0,
        Following = 1,
        Finding = 2
    }

    public enum ShootState
    {
        Idle = 0,
        Spraying = 1,
        Precise = 2
    }

    #endregion

    #region States

    public MoveState moveState = MoveState.Jumping;

    public PathFindState pathFindState = PathFindState.Wandering;

    public ShootState shootState = ShootState.Idle;

    #endregion

    void Update()
    {
        multiplayerBotJump.enabled = moveState == MoveState.Jumping;
    }
}