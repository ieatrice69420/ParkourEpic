using UnityEngine;
using System.Collections;
using static UnityEngine.Mathf;

public class MultiplayerBotObjective : MonoBehaviour
{
    public MultiplayerBotStateManager stateManager;
    [SerializeField]
    MultiplayerWayPoints objective;

    void Start()
    {
        StartCoroutine(ChangeTarget());
    }

    IEnumerator ChangeTarget()
    {
        while (true)
        {
            float distance = Infinity;

            for (int i = 0; i < objective.wayPointList.Count; i++)
            {
                Vector3 objectivePos = objective.wayPointList[i];
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