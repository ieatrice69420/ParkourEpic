using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using static System.Math;

public class MultiplayerBotZipline : BotClass
{
    Transform zipLineCarrier;
    public bool isZipLining = true;
    [SerializeField]
    float zipLineSpeed = 10f;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    Vector3 velocity;
    [SerializeField]
    float gravity = -10f;
    float actualJumpHeight;

    void Start() => actualJumpHeight = (float)Sqrt((double)(2f * -2f * gravity));

    private void OnEnable()
    {
        ShareVelocity(multiplayerBotStateManager.velocity, out velocity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Zipline"))
        {
            Zipline zipline = hit.transform.parent.gameObject.GetComponent<Zipline>();
            zipLineCarrier = hit.transform;
            zipline?.Move(zipLineSpeed, hit.point);
            isZipLining = true;
        }
    }

    private void Update()
    {
        if (isZipLining)
        {
            ZipLineMove();

            if (zipLineCarrier.GetComponent<ZiplineCarrier>().velocity == Vector3.zero)
            {
                StartCoroutine(DetachZipline());
            }
        }
        else
        {
            controller.Move(velocity * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
        }

        if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Jumping;

        controller.enabled = !isZipLining;
    }

    void ZipLineMove() => transform.position = zipLineCarrier.position;

    private void OnDisable()
    {
        controller.enabled = true;
        ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
    }

    IEnumerator DetachZipline()
    {
        velocity.y = actualJumpHeight;

        for (float f = 0f; f < 1f; f += Time.deltaTime)
        {
            isZipLining = false;
            yield return null;
        }
    }
}