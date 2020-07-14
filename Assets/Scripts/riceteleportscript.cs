using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riceteleportscript : MonoBehaviour
{
    [SerializeField]
    Vector3[] positions;
    public int maxPosCount;

    void OnTriggerEnter(Collider other) => transform.position = positions[Random.Range(0, maxPosCount)];
}
