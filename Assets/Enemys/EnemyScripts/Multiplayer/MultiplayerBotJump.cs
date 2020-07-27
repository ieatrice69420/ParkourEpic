using System.Collections;
using static System.Math;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.AI;

public class MultiplayerBotJump : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    CharacterController controller;
    Vector3 velocity;
    [SerializeField]
    float jumpHeight, gravity;
    float actualJumpHeight;
    bool isGrounded = true;
    Vector3 jumpDir;
    [SerializeField]
    MultiplayerBotStateManager multiplayerBotStateManager;
    public MultiplayerBotStats stats;

    void Start() => actualJumpHeight = (float)Sqrt((double)(jumpHeight * -2f * gravity));

    void OnEnable()
    {
        isGrounded = true;
    }

    void LateUpdate()
    {
        JumpCheck();
        Move();
        Gravity();
    }

    void Move()
    {
        if (isGrounded)
        {
            if (navMeshAgent.enabled == true && navMeshAgent.isOnNavMesh) navMeshAgent.SetDestination(multiplayerBotStateManager.desiredPosition);
        }
        else
        {
            controller.Move(jumpDir * navMeshAgent.speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded) velocity.y = -2f;
    }

    void JumpCheck()
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        if (data.valid) StartCoroutine(Jump(Vector3.Distance(data.startPos, data.endPos) / navMeshAgent.speed, stats.jumpDelay + Random.Range(stats.jumpDelayRangeMin, stats.jumpDelayRangeMax)));
    }

    public IEnumerator Jump(float duration, float delay)
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        jumpDir = new Vector3((data.endPos - data.startPos).normalized.x, 0f, (data.endPos - data.startPos).normalized.z);
        isGrounded = false;
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(Clamp(delay, 0f, .6f));
        if (controller.isGrounded) velocity.y = actualJumpHeight;
        yield return new WaitForSeconds(duration);
        while (true)
        {
            if (controller.isGrounded)
            {
                isGrounded = true;
                navMeshAgent.enabled = true;
                break;
            }
            yield return null;
        }
    }
}