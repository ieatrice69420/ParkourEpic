using UnityEngine;
using System.Collections;
using static UnityEngine.Mathf;

public class MultiplayerBotObjective : MonoBehaviour
{
    public MultiplayerBotStateManager stateManager;
    Objective objective;

    void Start()
    {
        objective = Objective.instance;
        StartCoroutine(ChangeTarget());
    }

    IEnumerator ChangeTarget()
    {
        while (true)
        {
            float distance = Infinity;

            for (int i = 0; i < objective.wayPoints.Length; i++)
            {
                Vector3 objectivePos = objective.wayPoints[i];
                float currentDis = (transform.position - objectivePos).sqrMagnitude;

                if (currentDis < distance)
                {
                    distance = currentDis;
                    stateManager.desiredPosition = objectivePos;
                }
            }

            yield return new WaitForSeconds(stateManager.stats.changeTargetTime);
        }
    }
}