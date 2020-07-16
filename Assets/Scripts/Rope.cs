using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]
    LineRenderer lr;
    [SerializeField]
    Vector3[] pos;
    [SerializeField]
    Transform tr1, tr2;
    [SerializeField]
    Rigidbody rb;

    void LateUpdate()
    {
    	pos[0] = tr1.position;
    	pos[1] = tr2.position;

    	lr.SetPositions(pos);
    }
}
