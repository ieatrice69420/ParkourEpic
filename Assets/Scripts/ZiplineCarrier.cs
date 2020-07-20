using UnityEngine;

public class ZiplineCarrier : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField]
    public Vector3 velocity;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void OnCollisonEnter(Collider other)
    {
        if (other.CompareTag("ZiplineStopper"))
        {
            velocity = Vector3.zero;
            StartCoroutine(Push.instance.DetachZipline());
            Debug.Log("ff");
        }
    }

    void LateUpdate()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
