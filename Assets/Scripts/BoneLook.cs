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
        transform.rotation = target.rotation;
    }
}
