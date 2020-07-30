using UnityEngine;
using static UnityEngine.Mathf;

public class MultiplayerBotObjective : MonoBehaviour
{
    public MultiplayerBotStateManager stateManager;
    Transform objective;

    void Start()
    {
        objective = Objective.instance.transform;
        InvokeRepeating("ChangeTarget", 0f, stateManager.stats.changeTargetTime);
    }

    void ChangeTarget()
    {
        float distance = Infinity;
        for (int i = 0; i < objective.childCount; i++)
        {
            Vector3 objectivePos = objective.GetChild(i).position;
            float currentDis = (transform.position - objectivePos).sqrMagnitude;
            
            if (currentDis < distance)
            {
                distance = currentDis;
                stateManager.desiredPosition = objectivePos;
            }
        }
    }
}