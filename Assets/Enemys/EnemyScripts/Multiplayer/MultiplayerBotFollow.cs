using UnityEngine;
using UnityEngine.AI;

public class MultiplayerBotFollow : BotClass
{
    public Transform target;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    MultiplayerBotStateManager stateManager;

    void Update()
    {
        stateManager.desiredPosition = target.position;
    }
}