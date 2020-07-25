using System.Collections;
using static System.Math;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float damage, fireRate, delay;
    float nextTimeToFire;
    ObjectPooler objectPooler;
    public bool isSeeingPlayer;
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

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        actualJumpHeight = (float)Sqrt((double)(jumpHeight * -2f * gravity));
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isSeeingPlayer)
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                StartCoroutine(Shoot(delay, damage));
            }
    }

    void LateUpdate()
    {
        JumpCheck();
        PathFind();
        Gravity();
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
    }

    void PathFind()
    {
        if (isGrounded)
        {
            if (isSeeingPlayer)
            {
                if (navMeshAgent.enabled == true && navMeshAgent.isOnNavMesh)
                navMeshAgent.SetDestination(player.position);
            }
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

    void Move()
    {
        // navMeshAgent.enabled = isGrounded ? true : false;
        // controller.enabled = isGrounded ? false : true;
    }

    void FixedUpdate()
    {
        if (!isSeeingPlayer)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
                if (hit.transform.CompareTag("Player"))
                    isSeeingPlayer = true;
        }
        else transform.LookAt(player);
    }

    IEnumerator Shoot(float duration, float dmg)
    {
        Vector3 shootDir = player.position - transform.position;
        yield return new WaitForSeconds(duration);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootDir, out hit))
        {
            Health health = hit.transform.gameObject.GetComponent<Health>();
            health?.SimpleTakeHealth(dmg);
            if (hit.transform.gameObject.layer == 10) objectPooler.SpawnBulletHole("Bullet Hole", hit.point, hit.normal);
        }
    }
}