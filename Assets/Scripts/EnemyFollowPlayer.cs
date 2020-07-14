using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform target, player;
    public float damage, fireRate, delay;
    float nextTimeToFire;
    ObjectPooler objectPooler;
    public bool isSeeingPlayer;
    [SerializeField]
    NavMeshAgent navMeshAgent;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => objectPooler = ObjectPooler.instance;

    private void Update()
    {
        if (isSeeingPlayer)
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                StartCoroutine(Shoot(delay, damage));
            }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (isSeeingPlayer)
            navMeshAgent.SetDestination(player.position);
    }

    void FixedUpdate()
    {
        if (!isSeeingPlayer)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 10))
                if (hit.transform.CompareTag("Player"))
                    isSeeingPlayer = true;
        }
        else
            transform.LookAt(target);
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
            if (hit.transform.gameObject.layer == 10)
                objectPooler.SpawnBulletHole("Bullet Hole", hit.point, hit.normal);
        }
    }
}
