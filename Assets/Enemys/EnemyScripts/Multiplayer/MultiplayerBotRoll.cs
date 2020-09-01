using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MultiplayerBotRoll : BotClass
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float duration;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    [SerializeField]
    float speed;
    [SerializeField]
    float defaultSpeed;

    void OnEnable()
    {
        agent.speed = speed;
        StartCoroutine(Roll());
    }

    IEnumerator Roll()
    {
        for (float timePassed = 0; timePassed < duration; timePassed += Time.deltaTime)
        {
            agent.SetDestination(multiplayerBotStateManager.desiredPosition);
            yield return null;
        }

        agent.speed = defaultSpeed;
        multiplayerBotStateManager.moveState = MoveState.Jumping;
    }
}