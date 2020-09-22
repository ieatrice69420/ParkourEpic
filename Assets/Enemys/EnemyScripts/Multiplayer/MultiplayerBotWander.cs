using UnityEngine;
using static UnityEngine.Vector3;
using static UnityEngine.Mathf;

public class MultiplayerBotWander : MonoBehaviour
{
    [SerializeField]
    MultiplayerBotStateManager stateManager;
    [SerializeField]
    MultiplayerWayPoints wayPoints;
    public Vector3 disabledWayPoint;

    void Update()
    {
        float currentDistance = Infinity;
        Vector3 target = transform.position;

        for (int i = 0; i < wayPoints.wayPointList.Count; i++)
        {
            float distance = Distance(transform.position, wayPoints.wayPointList[i]);

            if (distance < currentDistance && wayPoints.wayPointList[i] != disabledWayPoint)
            {
                currentDistance = distance;
                target = wayPoints.wayPointList[i];
            }

            if (distance < stateManager.stats.wayPointDisableDistance) disabledWayPoint = wayPoints.wayPointList[i];
        }

        Vector3 offset = new Vector3(Random.Range(stateManager.stats.wayPointInnaccuracy, stateManager.stats.wayPointInnaccuracy * -1f), 0f, Random.Range(stateManager.stats.wayPointInnaccuracy, stateManager.stats.wayPointInnaccuracy * -1f));
        stateManager.desiredPosition = target + offset;
    }
}