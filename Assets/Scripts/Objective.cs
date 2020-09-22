using UnityEngine;

public class Objective : MonoBehaviour
{
    public static Objective instance;
    public Vector3[] wayPoints;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i).position;
        }
    }
}