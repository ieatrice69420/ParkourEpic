using UnityEngine;
using System.Collections.Generic;

public class MultiplayerWayPoints : MonoBehaviour
{
    public List<Vector3> wayPointList = new List<Vector3>();
    [SerializeField]
    List<Vector3> disabledWayPointList = new List<Vector3>();

    private void Start()
    {
        if (wayPointList.Count > 0) wayPointList.Clear();

        foreach (Vector3 wayPoint in WayPointManager.instance.wayPoints) wayPointList.Add(wayPoint);
    }
}