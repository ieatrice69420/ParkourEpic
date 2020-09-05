using UnityEngine;
using UnityEngine.AI;

public class MultiplayerBotStill : BotClass
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    [SerializeField]
    float defaultSpeed;

    void OnEnable() => agent.speed = 0f;

    public void Return()
    {
        agent.speed = defaultSpeed;
        multiplayerBotStateManager.moveState = MoveState.Still;
    }
}