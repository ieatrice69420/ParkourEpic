using UnityEngine;

public class BoneLook : MonoBehaviour
{
    [SerializeField]
    Transform target;

    void LateUpdate()
    {
        Look();
    }

    void Look()
    {
        if (target != null) transform.rotation = target.rotation;
    }
}
