using UnityEngine;

public class BotClass : MonoBehaviour
{
    /// <summary>
    /// These states show what triggers the navmeshagent touch
    /// </summary>
    public enum TriggerState
    {
        Jump = 0,
        WallJump = 1,
        WallRun = 2
    }

    public virtual void ShareVelocity(Vector3 inputVelocity, out Vector3 outputVelocity)
    {
        outputVelocity = inputVelocity;
    }
}