using UnityEngine;
using UnityEngine.AI;

public class MultiplayerBotFollow : BotClass
{
    public Transform target;
    [SerializeField]
    NavMeshAgent agent;

    void Update()
    {
        agent.SetDestination(target.position);
    }
}