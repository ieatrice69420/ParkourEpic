using UnityEngine;

public class BotClass : MonoBehaviour
{
    #region Enums

    /// <summary>
    /// These states show what triggers the navmeshagent touch
    /// </summary>
    public enum TriggerState
    {
        Jump = 0,
        WallJump = 1,
        WallRun = 2
    }

    /// <summary>
    /// These states affect the way the AI is moving, but do not control direction of movement.
    /// </summary>
    public enum MoveState
    {
        Jumping = 0,
        WallRunning = 1,
        Still = 2
    }

    /// <summary>
    /// These states change the direction of movement.
    /// </summary>
    public enum PathFindState
    {
        Wandering = 0,
        Following = 1,
        Objective = 2
    }

    /// <summary>
    /// These states affect the shooting of the AI
    /// </summary>
    public enum ShootState
    {
        Idle = 0,
        Spraying = 1,
        Precise = 2
    }

    #endregion

    public virtual void ShareVelocity(Vector3 inputVelocity, out Vector3 outputVelocity)
    {
        outputVelocity = inputVelocity;
    }
}