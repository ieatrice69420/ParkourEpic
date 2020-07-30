using UnityEngine;
using static UnityEngine.Vector3;
using static UnityEngine.Mathf;

public class MultiplayerBotWander : MonoBehaviour
{
    [SerializeField]
    MultiplayerBotStateManager stateManager;
    WayPointManager wayPoints;
    public Vector3 disabledWayPoint;

    void Start() => wayPoints = WayPointManager.instance;

    void Update()
    {
        float currentDistance = Infinity;
        Vector3 target = transform.position;

        for (int i = 0; i < wayPoints.wayPoints.Length; i++)
        {
            float distance = Distance(transform.position, wayPoints.wayPoints[i]);

            if (distance < currentDistance && wayPoints.wayPoints[i] != disabledWayPoint)
            {
                currentDistance = distance;
                target = wayPoints.wayPoints[i];
            }

            if (distance < stateManager.stats.wayPointDisableDistance) disabledWayPoint = wayPoints.wayPoints[i];
        }

        Vector3 offset = new Vector3(Random.Range(stateManager.stats.wayPointInnaccuracy, stateManager.stats.wayPointInnaccuracy * -1f), 0f, Random.Range(stateManager.stats.wayPointInnaccuracy, stateManager.stats.wayPointInnaccuracy * -1f));
        stateManager.desiredPosition = target + offset;
    }
}