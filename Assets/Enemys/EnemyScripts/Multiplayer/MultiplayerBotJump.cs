using System.Collections;
using static System.Math;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.AI;

public class MultiplayerBotJump : MonoBehaviour
{
    public Transform player;
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

    void OnEnable()
    {
        actualJumpHeight = (float)Sqrt((double)(jumpHeight * -2f * gravity));
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
            if (navMeshAgent.enabled == true && navMeshAgent.isOnNavMesh) navMeshAgent.SetDestination(player.position);
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
        if (data.valid) StartCoroutine(Jump(Vector3.Distance(data.startPos, data.endPos) / navMeshAgent.speed));
    }

    public IEnumerator Jump(float duration)
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        jumpDir = new Vector3((data.endPos - data.startPos).normalized.x, 0f, (data.endPos - data.startPos).normalized.z);
        isGrounded = false;
        navMeshAgent.enabled = false;
        velocity.y = actualJumpHeight;
        yield return new WaitForSeconds(duration);
        navMeshAgent.enabled = true;
        isGrounded = true;
    }
}