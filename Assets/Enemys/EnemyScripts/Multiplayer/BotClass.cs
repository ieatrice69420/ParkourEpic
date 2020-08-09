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
}