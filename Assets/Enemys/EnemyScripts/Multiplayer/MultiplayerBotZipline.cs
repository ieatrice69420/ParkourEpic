using UnityEngine;
using UnityEngine.AI;

public class MultiplayerBotZipline : BotClass
{
    Transform zipLineCarrier;
    public bool isZipLining;
    [SerializeField]
    float zipLineSpeed = 10f;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    Vector3 velocity;
    [SerializeField]
    float gravity = -10f;

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

            if (zipLineCarrier.GetComponent<ZiplineCarrier>().velocity == Vector3.zero) isZipLining = false;
        }
        else
        {
            controller.Move(velocity * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
        }

        if (controller.isGrounded) multiplayerBotStateManager.moveState = MoveState.Ziplining;
    }

    void ZipLineMove() => transform.position = zipLineCarrier.position;

    private void OnDisable()
    {
        ShareVelocity(velocity, out multiplayerBotStateManager.velocity);
    }
}