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
            float currentDis = Vector3.Distance(transform.position, objectivePos);
            if (currentDis < distance)
            {
                distance = currentDis;
                print("aaaaaaaa");
                stateManager.desiredPosition = objectivePos;
            }
        }
    }
}