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
    }

    private void Move() => transform.Translate(velocity * Time.deltaTime);

    void Stop()
    {
        Push.instance.isZipLining = false;
        velocity = Vector3.zero;
        canBeStopped = false;
    }
}