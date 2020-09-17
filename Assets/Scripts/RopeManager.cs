using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RopeManager : MonoBehaviour
{
    [SerializeField]
    Transform[] ropeTipsSetInInspector;
    public static List<Transform> ropeTips = new List<Transform>();

    private void OnEnable() => ropeTips = ropeTipsSetInInspector.ToList<Transform>();
}