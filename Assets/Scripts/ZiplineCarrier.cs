using UnityEngine;

public class ZiplineCarrier : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 velocity;
    [SerializeField]
    Transform[] barriers;
    [SerializeField]
    float collisionDistance;
    public bool canBeStopped = true;
    [SerializeField]
    LayerMask stopperMask;
    [SerializeField]
    Transform stopperStart, stopperEnd;

    void Start() => startPos = transform.localPosition;

    void LateUpdate()
    {
        Move();
        CollisionCheck();
    }

    void CollisionCheck()
    {
        if (canBeStopped)
            foreach (Transform barrier in barriers)
                if (Vector3.SqrMagnitude(transform.position - barrier.position) <= collisionDistance * collisionDistance)
                    Stop();

        transform.position = new Vector3
        (
            Helper.Mathf.Clamp(transform.position.x, stopperStart.position.x, stopperEnd.position.x),
            transform.position.y,
            Helper.Mathf.Clamp(transform.position.z, stopperStart.position.z, stopperEnd.position.z)
        );
    }

    private void Move() => transform.Translate(velocity * Time.deltaTime);

    void Stop()
    {
        velocity = Vector3.zero;
        canBeStopped = false;
    }
}