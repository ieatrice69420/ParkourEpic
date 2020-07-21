using UnityEngine;

public class ZiplineCarrier : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 velocity;
    [SerializeField]
    new Rigidbody rigidbody;

    void Start() => startPos = transform.localPosition;

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
        Move();
        Debug.Log("rigidbody.issleeping = " + rigidbody.IsSleeping());
    }

    private void Move() => transform.Translate(velocity * Time.deltaTime);
}