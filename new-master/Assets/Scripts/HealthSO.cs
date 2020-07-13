using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health")]
public class HealthSO : ScriptableObject
{
    public Vector3 headShotStart;
    public bool isMultiplayer;
    public bool isRealPlayer;
}
